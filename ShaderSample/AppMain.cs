using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

namespace ShaderSample
{
	public class AppMain
	{
		private static GraphicsContext graphics;
		//private static ShaderProgram shaderTile;
		//private static ShaderProgram shaderDanmaku;
		private static ShaderProgram shaderStar;
		//private static ShaderProgram shaderNoise;
		
		//from sample2.1
		static float[] vertices=new float[12];

		static float[] texcoords;

		static float[] colors = {
			1.0f,	1.0f,	1.0f,	1.0f,	// 0 top left.
			1.0f,	1.0f,	1.0f,	1.0f,	// 1 bottom left.
			1.0f,	1.0f,	1.0f,	1.0f,	// 2 top right.
			1.0f,	1.0f,	1.0f,	1.0f,	// 3 bottom right.
		};

		const int indexSize = 4;
		static ushort[] indices;

		static VertexBuffer vertexBuffer;

		static Matrix4 screenMatrix;
		//end
		
		static DateTime StartTime;

		
		public static void Main (string[] args)
		{
			Initialize ();

			while (true) {
				SystemEvents.CheckEvents ();
				Update ();
				Render ();
			}
		}

		public static void Initialize ()
		{
			// Set up the graphics system
			graphics = new GraphicsContext ();
			
			//shaderTile= new ShaderProgram( "/Application/shaders/TileBritish.cgx" );
			//shaderDanmaku=new ShaderProgram("/Application/shaders/DanmakuRepeatLimited.cgx");
			shaderStar=new ShaderProgram("/Application/shaders/StarIntegerWaveTwinkle.cgx");
			//shaderNoise=new ShaderProgram("/Application/shaders/NoiseColor.cgx");

			texcoords = new float[]{
			0.0f, 0.0f,	// 0 top left.
			0.0f, 544.0f,	// 1 bottom left.
			960.0f, 0.0f,	// 2 top right.
			960.0f, 544.0f,	// 3 bottom right.
		};
			
			//sample
			vertices[0]=0.0f;	// x0
			vertices[1]=0.0f;	// y0
			vertices[2]=0.0f;	// z0
			
			vertices[3]=0.0f;	// x1
			vertices[4]=graphics.Screen.Height;	// y1
			vertices[5]=0.0f;	// z1
			
			vertices[6]=graphics.Screen.Width;	// x2
			vertices[7]=0.0f;	// y2
			vertices[8]=0.0f;	// z2
			
			vertices[9]=graphics.Screen.Width;	// x3
			vertices[10]=graphics.Screen.Height;	// y3
			vertices[11]=0.0f;	// z3
			

			indices = new ushort[indexSize];
			indices[0] = 0;
			indices[1] = 1;
			indices[2] = 2;
			indices[3] = 3;
			
			//												vertex pos,               texture,       color
			vertexBuffer = new VertexBuffer(4, indexSize, VertexFormat.Float3, VertexFormat.Float2, VertexFormat.Float4);
			

			vertexBuffer.SetVertices(0, vertices);
			vertexBuffer.SetVertices(1, texcoords);
			vertexBuffer.SetVertices(2, colors);
			
			vertexBuffer.SetIndices(indices);
			graphics.SetVertexBuffer(0, vertexBuffer);

			screenMatrix = new Matrix4(
				 2.0f/graphics.Screen.Width,	0.0f,	    0.0f, 0.0f,
				 0.0f,   -2.0f/graphics.Screen.Height,	0.0f, 0.0f,
				 0.0f,   0.0f, 1.0f, 0.0f,
				 -1.0f,  1.0f, 0.0f, 1.0f
			);
			//end
			
			StartTime=DateTime.Now;
		}

		public static void Update ()
		{
			// Query gamepad for current state
			var gamePadData = GamePad.GetData (0);
		}

		public static void Render ()
		{
			// Clear the screen
			graphics.SetClearColor (0.0f, 0.0f, 0.0f, 0.0f);
			graphics.Clear ();

			/*
			graphics.SetShaderProgram(shaderTile);

			shaderTile.SetUniformValue(shaderTile.FindUniform("WorldViewProj"),ref screenMatrix);

			Vector2 tileSize=new Vector2(10.0f,10.0f);
			shaderTile.SetUniformValue(shaderTile.FindUniform("TileSize"),ref tileSize);
			Vector2 jointSize=new Vector2(1f,1f);
			shaderTile.SetUniformValue(shaderTile.FindUniform("JointSize"),ref jointSize);
			Vector4 tileColor=new Vector4(156.0f/255.0f,75.0f/255.0f,54.0f/255.0f,1.0f);
			shaderTile.SetUniformValue(shaderTile.FindUniform("TileColor"),ref tileColor);
			Vector4 jointColor=new Vector4(0.0f,0.0f,0.0f,1.0f);
			shaderTile.SetUniformValue(shaderTile.FindUniform("JointColor"),ref jointColor);
			*/
			//shaderTile.SetUniformValue(0,ref screenMatrix);
			
			/*
			graphics.SetShaderProgram(shaderDanmaku);
			shaderDanmaku.SetUniformValue(shaderDanmaku.FindUniform("WorldViewProj"),ref screenMatrix);
			
			float EllapsedTime=(float)((DateTime.Now-StartTime).TotalSeconds);
			//if(EllapsedTime<0.3f){EllapsedTime*=4.0f;}else{EllapsedTime+=0.9f;}
			//if(EllapsedTime>10.0f & EllapsedTime<20.0f){EllapsedTime=20.0f-EllapsedTime;}
			//if(EllapsedTime>5.0f){EllapsedTime=5.0f;}
			//EllapsedTime/=5.0f;
			shaderDanmaku.SetUniformValue(shaderDanmaku.FindUniform("EllapsedTime"),EllapsedTime);
			Vector2 ExplosionPoint=new Vector2(480.0f,272.0f);
			shaderDanmaku.SetUniformValue(shaderDanmaku.FindUniform("ExplosionPoint"),ref ExplosionPoint);
			shaderDanmaku.SetUniformValue(shaderDanmaku.FindUniform("BulletSize"),10.0f);
			shaderDanmaku.SetUniformValue(shaderDanmaku.FindUniform("BulletSpeed"),150.0f);
			shaderDanmaku.SetUniformValue(shaderDanmaku.FindUniform("BulletRotationSpeed"),0.8f);
			shaderDanmaku.SetUniformValue(shaderDanmaku.FindUniform("BulletCount"),15);
			Vector4 bgColor=new Vector4(0.0f,0.0f,0.0f,1.0f);
			shaderDanmaku.SetUniformValue(shaderDanmaku.FindUniform("BackGroundColor"),ref bgColor);
			Vector4 bulletColor=new Vector4(0.0f,0.0f,0.9f,1.0f);
			shaderDanmaku.SetUniformValue(shaderDanmaku.FindUniform("BulletColor"),ref bulletColor);
			Vector4 bulletColorCenter=new Vector4(0.5f,0.5f,1.0f,1.0f);
			shaderDanmaku.SetUniformValue(shaderDanmaku.FindUniform("BulletColorCenter"),ref bulletColorCenter);
			shaderDanmaku.SetUniformValue(shaderDanmaku.FindUniform("RepeatTime"),0.1f);
			shaderDanmaku.SetUniformValue(shaderDanmaku.FindUniform("RepeatCountLimit"),-1);
			shaderDanmaku.SetUniformValue(shaderDanmaku.FindUniform("RepeatRotationDifference"),1.0f);

			Vector2 bulletRotationLimit=new Vector2(1.57f,3.4f);
			shaderDanmaku.SetUniformValue(shaderDanmaku.FindUniform("BulletRotationLimit"),ref bulletRotationLimit);
			*/
			
			graphics.SetShaderProgram(shaderStar);

			shaderStar.SetUniformValue(shaderStar.FindUniform("WorldViewProj"),ref screenMatrix);
			
			Vector4 StarBgColor=new Vector4(0.0f,0.0f,0.0f,1.0f);
			shaderStar.SetUniformValue(shaderStar.FindUniform("BgColor"),ref StarBgColor);
			Vector4 StarStarColor=new Vector4(1.0f,1.0f,0.0f,1.0f);
			shaderStar.SetUniformValue(shaderStar.FindUniform("StarColor"),ref StarStarColor);
			float StarEllapsedTime=(float)((DateTime.Now-StartTime).TotalSeconds);
			shaderStar.SetUniformValue(shaderStar.FindUniform("OffsetX"),(int)(StarEllapsedTime*300.0f));
			shaderStar.SetUniformValue(shaderStar.FindUniform("OffsetY"),(int)(StarEllapsedTime*000.0f));
			shaderStar.SetUniformValue(shaderStar.FindUniform("Possibility"),0.002f);

			Vector2 WaveCoord=new Vector2(300.0f,272.0f);
			shaderStar.SetUniformValue(shaderStar.FindUniform("WaveCoord"),ref WaveCoord);
			shaderStar.SetUniformValue(shaderStar.FindUniform("WaveTime"),StarEllapsedTime-2.0f);
			shaderStar.SetUniformValue(shaderStar.FindUniform("WaveSpeed"),200.0f);
			shaderStar.SetUniformValue(shaderStar.FindUniform("WaveScale"),50.0f);
			shaderStar.SetUniformValue(shaderStar.FindUniform("WaveFrequency"),2.0f);
			shaderStar.SetUniformValue(shaderStar.FindUniform("WaveLastTime"),5.0f);
			shaderStar.SetUniformValue(shaderStar.FindUniform("TwinkleTime"),StarEllapsedTime);
			
			shaderStar.SetUniformValue(shaderStar.FindUniform("Seed"),2);
			
			/*
			graphics.SetShaderProgram(shaderNoise);
			
			shaderNoise.SetUniformValue(shaderNoise.FindUniform("WorldViewProj"),ref screenMatrix);
			float NoiseEllapsedTime=(float)((DateTime.Now-StartTime).TotalSeconds);
			shaderNoise.SetUniformValue(shaderNoise.FindUniform("Time"),(int)(NoiseEllapsedTime*60));
			*/

			graphics.DrawArrays(DrawMode.TriangleStrip, 0, indexSize);
	
			// Present the screen
			graphics.SwapBuffers ();
		}
	}
}
