﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zarodkowanie
{
    class GravityCell
    {
        private float gravityX;
        private float gravityY;
        private float energy;
        private int value;
        private int positionX;
        private int positionY;
        private float dislocationDensity;
        private bool recrystalizzed;
        private bool lastRecrystalized;

        public GravityCell(float gX, float gY, int v, int pX, int pY)
        {
            this.gravityX = gX;
            this.gravityY = gY;
            this.value = v;
            this.energy = 0;
            positionX = pX;
            positionY = pY;
            dislocationDensity = 0;
            recrystalizzed = false;
            lastRecrystalized = false;
        }

        public float getDislocationDensity()
        {
            return dislocationDensity;
        }

        public void setDislocationDensity(float d)
        {
            dislocationDensity = d;
        }

        public bool IsRecrystalized()
        {
            return recrystalizzed;
        }

        public void setRecrystalized(bool r)
        {
            this.recrystalizzed = r;
        }

        public bool IsLastRecrystalized()
        {
            return lastRecrystalized;
        }

        public void setLastRecrystalized(bool r)
        {
            this.lastRecrystalized = r;
        }

        public int GetPositionX()
        {
            return this.positionX;
        }

        public int GetPositionY()
        {
            return this.positionY;
        }

        public void SetEnergy(float ene)
        {
            this.energy = ene;
        }

        public float GetEnergy()
        {
            return this.energy;
        }

        public void SetValue(int v)
        {
            this.value = v;
        }

        public int GetValue()
        {
            return this.value;
        }

        public float GetGravityX()
        {
            return this.gravityX;
        }

        public float GetGravityY()
        {
            return this.gravityY;
        }
    }
}
