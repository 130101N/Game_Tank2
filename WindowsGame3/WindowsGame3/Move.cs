using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame3
{
    class Move
    {
        private int[] position;
    private Texture2D image;
    ContentManager content;
    public Move(){
        position = new int[2];
        position[0] = 0; // X
        position[1] = 0; // Y
        //image = content.Load<Texture2D>("TankRush");
            
    }

    public void setPosition(int x, int y){
        this.position[0] = x;
        this.position[1] = y;
    }

    public int[] getPosition(){
        return this.position;
    }

    public int getX(){
        return this.position[0];
    }

    public int getY(){
        return this.position[1];
    }

    public Texture2D getImage(){
        return this.image;
    }

    public void moveTo(int x, int y){
        while(this.position[0]!=x && this.position[1]!=y){
            if(this.position[0]<x){
                this.position[0]++;
            }
            if(this.position[1]<y){
                this.position[1]++;
            }
        }
        Console.WriteLine(position[1]);


    }
        
        
    }
}
