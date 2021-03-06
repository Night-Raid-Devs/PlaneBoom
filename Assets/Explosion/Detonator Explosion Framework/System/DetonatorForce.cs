using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Detonator))]
[AddComponentMenu("Detonator/Force")]
public class DetonatorForce : DetonatorComponent 
{
	private float _baseRadius = 50.0f;
	private float _basePower = 4000.0f;
	private float _scaledRange;
	private float _scaledIntensity;
	private bool _delayedExplosionStarted = false;
	private float _explodeDelay;
	
	public float radius;
	public float power;
	public GameObject fireObject;
	public float fireObjectLife;
	
	private Collider[] _colliders;
	private GameObject _tempFireObject;

    public AudioClip[] nearSounds;
    public AudioClip[] farSounds;
    public float distanceThreshold = 50f; //threshold in m between playing nearSound and farSound
    private AudioSource _soundComponent;

    private bool isPlayerRocket;

    override public void Init()
	{
        _soundComponent = (AudioSource)gameObject.AddComponent<AudioSource>();
    }

	void Update()
	{
        if (_soundComponent != null) _soundComponent.pitch = Time.timeScale;

        if (_delayedExplosionStarted)
		{
			_explodeDelay = (_explodeDelay - Time.deltaTime);
			if (_explodeDelay <= 0f)
			{
				Explode();
			}
		}
	}
	
    public void SetIsPlayerRocket(bool isPlayerRocket)
    {
        this.isPlayerRocket = isPlayerRocket;
    }

	private Vector3 _explosionPosition;
    private int _idx;
    override public void Explode()
	{
		if (!on) return;
		if (detailThreshold > detail) return;	
		
		if (!_delayedExplosionStarted)
		{
			_explodeDelay = explodeDelayMin + (Random.value * (explodeDelayMax - explodeDelayMin));
		}
		if (_explodeDelay <= 0) //if the delayTime is zero
		{
			//tweak the position such that the explosion center is related to the explosion's direction
			_explosionPosition = transform.position; //- Vector3.Normalize(MyDetonator().direction);
			_colliders = Physics.OverlapSphere (_explosionPosition, radius);

			foreach (Collider hit in _colliders) 
			{
				if (hit.GetComponent<Rigidbody>() && hit.name != "AircraftJet")
				{
                    //align the force along the object's rotation
                    //this is wrong - need to attenuate the velocity according to distance from the explosion center			
                    //offsetting the explosion force position by the negative of the explosion's direction may help
                    if (isPlayerRocket)
                    {
                        if (hit.tag != "Player")
                        {
                            hit.GetComponent<Rigidbody>().AddExplosionForce((power * size), _explosionPosition, (radius * size), (4f * MyDetonator().upwardsBias * size));
                        }
                    }
                    else
                    {
                        if (hit.tag != "enemy")
                        {
                            hit.GetComponent<Rigidbody>().AddExplosionForce((power * size), _explosionPosition, (radius * size), (4f * MyDetonator().upwardsBias * size));
                        }
                    }

                    //fixed 6/15/2013 - didn't work before, was sending message to this script instead :)
                    //Debug.Log("OnDetonatorForceHit " + isPlayerRocket + " " + hit.name);
                    hit.gameObject.SendMessage("OnDetonatorForceHit", isPlayerRocket, SendMessageOptions.DontRequireReceiver);
					
					//and light them on fire for Rune
					if (fireObject)
					{
						//check to see if the object already is on fire. being on fire twice is silly
						if (hit.transform.Find(fireObject.name+"(Clone)"))
						{
							return;
						}
						
						_tempFireObject = (Instantiate(fireObject, this.transform.position, this.transform.rotation)) as GameObject;
						_tempFireObject.transform.parent = hit.transform;
						_tempFireObject.transform.localPosition = new Vector3(0f,0f,0f);
						if (_tempFireObject.GetComponent<ParticleEmitter>())
						{
							_tempFireObject.GetComponent<ParticleEmitter>().emit = true;
							Destroy(_tempFireObject,fireObjectLife);
						}
					}
				}
			}

            if (_soundComponent != null)
            {
                //		_soundComponent.minVolume = minVolume;
                //		_soundComponent.maxVolume = maxVolume;
                //		_soundComponent.rolloffFactor = rolloffFactor;
                try
                {
                    if (Vector3.Distance(Camera.main.transform.position, this.transform.position) < distanceThreshold)
                    {
                        _idx = (int)(Random.value * nearSounds.Length);
                        _soundComponent.PlayOneShot(nearSounds[_idx]);
                    }
                    else
                    {
                        _idx = (int)(Random.value * farSounds.Length);
                        _soundComponent.PlayOneShot(farSounds[_idx]);
                    }
                }
                catch
                {
                }
            }

            _delayedExplosionStarted = false;
			_explodeDelay = 0f;
		}
		else
		{
			//tell update to start reducing the start delay and call explode again when it's zero
			_delayedExplosionStarted = true;
		}
	}
	
	public void Reset()
	{
		radius = _baseRadius;
		power = _basePower;
	}
}

