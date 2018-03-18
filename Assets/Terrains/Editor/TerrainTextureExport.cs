using UnityEditor;
using UnityEngine;
using System.IO;


/// <summary>
/// Terrain texture export.
/// </summary>
public class TerrainTextureExport : EditorWindow
{	
	private Terrain mTerrain;
	
	private int mAlphaMapSizeX = 0;
	private int mAlphaMapSizeY = 0;
	private int mAlphaMapLayers = 0;
	
	private Texture2D[] mTextures;
	private int mNeedTextures;
	
	private bool mCanStartExporting = true;
	
	// Add menu item named "My Window" to the Window menu
	[MenuItem("Dvornik/Terrain/Save texture map")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(TerrainTextureExport));			
	}

	void OnGUI()
	{
		GUILayout.Label( "Terrain texturing export tool" );
		
		mTerrain = (Terrain) EditorGUILayout.ObjectField(mTerrain, typeof(Terrain), true);
		
		if ( mTerrain != null )
		{
			GUILayout.Label( "Alpha map resolution:" + mTerrain.terrainData.alphamapWidth + "x" + mTerrain.terrainData.alphamapHeight );
			
			mAlphaMapSizeX = mTerrain.terrainData.alphamapWidth;
			mAlphaMapSizeY = mTerrain.terrainData.alphamapHeight;	
			
			mAlphaMapLayers = mTerrain.terrainData.alphamapLayers;
			GUILayout.Label( "Alpha map layers:" + mAlphaMapLayers );
			
			mNeedTextures = mTerrain.terrainData.alphamapLayers / 4 + 1;
			
			if ( mTextures == null )
			{
				mTextures = new Texture2D[mNeedTextures];
			}
			
			if ( mAlphaMapLayers > 0 )
			{
				if ( GUILayout.Button("Start exporting") )
				{
					DoExporting();
				}
			}
			
		}
		
						
	}
	
	/// <summary>
	/// Dos the exporting.
	/// </summary>
	private void DoExporting()
	{
		
		float[,,] map = mTerrain.terrainData.GetAlphamaps( 0,0, mAlphaMapSizeX, mAlphaMapSizeY );
		
		for( int i=0;i<mNeedTextures;i++)
		{
			mTextures[i] = new Texture2D( mAlphaMapSizeX,  mAlphaMapSizeY );
		}
					
		for( int y=0;y<mAlphaMapSizeY;y++)
		{
			
			EditorUtility.DisplayProgressBar( "Texture tool", "Create color map", (float) y / mAlphaMapSizeY );
			
			for( int x=0;x<mAlphaMapSizeX;x++)
			{												
											
				for ( int i=0;i< mAlphaMapLayers;i+=4)
				{												
					Color col = new Color( 0f, 0f, 0f, 1f );

					//
					if ( i < mAlphaMapLayers )
					{						
						col.r = map[x,y,i];
					}

					if ( (i + 1) < mAlphaMapLayers )
					{
						col.g = map[x,y,i+1];
					}

					if ( (i+2) < mAlphaMapLayers )
					{
						col.b = map[x,y,i+2];
					}

					if ( (i+3) < mAlphaMapLayers )
					{
						col.a = map[x,y,i+3];
					}
																		
					mTextures[ i / 4 ].SetPixel( x, y, col );
					
				}				
			}	
		}
		
		EditorUtility.ClearProgressBar();
		
		SaveTextures();
		
	}
	
	/// <summary>
	/// Saves the textures.
	/// </summary>
	private void SaveTextures()
	{	
				
		for( int i=0;i< mNeedTextures;i++)
		{
			byte[] data = mTextures[i].EncodeToPNG();			
			File.WriteAllBytes( Application.dataPath + "/" + mTerrain.name + "_Col_" + i + ".png", data );			
		}
		
		AssetDatabase.Refresh();		

	}
	
	
}
