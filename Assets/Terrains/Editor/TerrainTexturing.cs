using UnityEditor;
using UnityEngine;

/// <summary>
/// Terrain texturing.
/// </summary>
public class TerrainTexturing : EditorWindow
{	
	private Terrain mTerrain;
	
	private int mAlphaMapSizeX = 0;
	private int mAlphaMapSizeY = 0;
	private int mAlphaMapLayers = 0;
	
	private Texture2D[] mTextures;
	
	private bool mCanStartTexturing = true;
	
	// Add menu item named "My Window" to the Window menu
	[MenuItem("Dvornik/Terrain/Load texture map")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(TerrainTexturing));			
	}

	void OnGUI()
	{
		GUILayout.Label( "Terrain texturing tool" );
		
		mTerrain = (Terrain) EditorGUILayout.ObjectField(mTerrain, typeof(Terrain), true);
		
		if ( mTerrain != null )
		{
			GUILayout.Label( "Alpha map resolution:" + mTerrain.terrainData.alphamapWidth + "x" + mTerrain.terrainData.alphamapHeight );
			
			mAlphaMapSizeX = mTerrain.terrainData.alphamapWidth;
			mAlphaMapSizeY = mTerrain.terrainData.alphamapHeight;	
			
			mAlphaMapLayers = mTerrain.terrainData.alphamapLayers;
			GUILayout.Label( "Alpha map layers:" + mAlphaMapLayers );
			
			int needTextures = 1;

			if ( mTerrain.terrainData.alphamapLayers > 0 && mTerrain.terrainData.alphamapLayers < 5 )
			{
				needTextures = 1;
			}
			else if ( mTerrain.terrainData.alphamapLayers > 4 && mTerrain.terrainData.alphamapLayers < 9 )
			{
				needTextures = 2;
			}
			else if ( mTerrain.terrainData.alphamapLayers > 8 && mTerrain.terrainData.alphamapLayers < 13 )
			{
				needTextures = 3;
			}
			else if ( mTerrain.terrainData.alphamapLayers > 12 && mTerrain.terrainData.alphamapLayers < 17 )
			{
				needTextures = 3;
			}

			if ( mTextures == null )
			{
				mTextures = new Texture2D[needTextures];
			}
			
			GUILayout.Label( "Textures:"  );
			
			for ( int i=0;i<needTextures;i++)
			{
				mTextures[i] = (Texture2D) EditorGUILayout.ObjectField(mTextures[i], typeof(Texture2D), true);
			}	
			
			//Check all conditions
			mCanStartTexturing = true;
			for( int i=0;i<needTextures;i++)
			{
				if ( mTextures[i] == null || mTextures[i].width != mAlphaMapSizeX || mTextures[i].height != mAlphaMapSizeY )
				{
					mCanStartTexturing = false;
				}										
			}
			
			if ( mCanStartTexturing )
			{
				if ( GUILayout.Button("Start texturing") )
				{
					DoTexturing();
				}
			}					
		}
		
						
	}
	
	/// <summary>
	/// Dos the texturing.
	/// </summary>
	private void DoTexturing()
	{
		float[,,] map = new float[ mAlphaMapSizeX, mAlphaMapSizeY, mAlphaMapLayers ];
		
		for( int y=0;y<mAlphaMapSizeY;y++)
		{
			for( int x=0;x<mAlphaMapSizeX;x++)
			{												
				for ( int i=0;i< mAlphaMapLayers;i++)
				{	
					//
					if ( i%4 == 0 )
					{	
						map[x,y,i] = mTextures[ i / 4 ].GetPixel( x, y ).r;				
					}
					else if ( i%4 == 1 )
					{
						map[x,y,i] = mTextures[ i / 4 ].GetPixel( x, y ).g;
					}
					else if ( i%4 == 2 )
					{
						map[x,y,i] = mTextures[ i / 4 ].GetPixel( x, y ).b;
					}
					else if ( i%4 == 3 )
					{
						map[x,y,i] = mTextures[ i / 4 ].GetPixel( x, y ).a;
					}					
				}				
			}	
		}
		
		// Apply generated alphamap
		mTerrain.terrainData.SetAlphamaps(0,0, map );			
	}
	
}
