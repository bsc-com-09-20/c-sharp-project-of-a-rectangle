using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Hello {
    class Game : GameWindow
    {
        int VertexShader;
        int FragmentShader;
        int ShaderProgram;
        int VertexBufferObject;
        int ElementBufferObject;
        int vertexArrayObject;

        [Obsolete]
        public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (width, height), Title = title}) { 
         
        }

        protected override void OnLoad() 
        {
            GL. ClearColor(System.Drawing.Color.Black);

            //vertex shader
            string vertexShaderSource = @"
            #version 330 core
            layout (location = 0) in vec3 aPos;

            out vec4 vertexColor;
            
            void main()
            {
                gl_Position = vec4(aPos, 1.0);
                vertexColor = vec4(1.0, 1.0, 0.0, 0.0);
            }";

            VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, vertexShaderSource);
            GL.CompileShader(VertexShader);

            // Fragment Shader
            string fragmentShaderSource = @"
            #version 330 core
            out vec4 FragColor;

            in vec4 vertexColor;
            
            void main() 
            {
                FragColor = vertexColor; // orange color
            }"; 

            FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL. ShaderSource(FragmentShader, fragmentShaderSource);
            GL.CompileShader(FragmentShader);

            // Shader Program
            ShaderProgram = GL.CreateProgram();
            GL.AttachShader(ShaderProgram, VertexShader);
            GL.AttachShader(ShaderProgram, FragmentShader);
            GL.LinkProgram(ShaderProgram);
            GL.UseProgram(ShaderProgram);

            //Vertex data
            float[] vertices = {
               0.5f, 0.5f, 0.0f,    //top right
               0.5f, -0.5f, 0.0f,   //bottom right
              -0.5f, -0.5f, 0.0f,   //bottom left
              -0.5f, 0.5f, 0.0f     //top left
            };

            // VAO and VBO setup
            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

         
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);

            base.OnLoad();
        
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            GL.UseProgram(ShaderProgram);
            GL.BindVertexArray(vertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
            base.OnRenderFrame(e);
        }
         uint[] indices = { //note that we start from 0!
            0, 1, 3,    // first triangle
            1, 2, 3     // second triangle
            };

     /*   protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (Keyboard.GetState().IsKeyDown(Key.Escape))
            Exit();

            base.OnUpdateFrame(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            GL.DeleteBuffer(VertexBufferObject);
            GL.DeleteVertexArray(vertexArrayObject);
            GL.DeleteShader(VertexShader);
            GL.DeleteShader(FragmentShader);
            GL.DeleteProgram(ShaderProgram);

            base.UnUnload(e);           
        } */
    }
}