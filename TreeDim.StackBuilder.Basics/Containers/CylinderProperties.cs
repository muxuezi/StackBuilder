﻿#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Sharp3D.Math.Core;
#endregion

namespace treeDiM.StackBuilder.Basics
{
    public class CylinderProperties : PackableNamed
    {
        #region Data members
        protected double _radiusOuter = 0.0, _radiusInner = 0.0;
        private double _height = 0.0;
        private Color _colorTop;
        private Color _colorWallOuter, _colorWallInner;
        #endregion

        #region Constructor
        public CylinderProperties(Document document)
            : base(document)
        {
        }
        public CylinderProperties(Document document, string name, string description
            , double radiusOuter, double radiusInner, double height, double weight
            , Color colorTop, Color colorWallOuter, Color colorWallInner)
            : base(document, name, description)
        {
            _radiusOuter = radiusOuter;
            _radiusInner = radiusInner;
            _height = height;
            SetWeight(weight);
            _colorTop = colorTop;
            _colorWallOuter = colorWallOuter;
            _colorWallInner = colorWallInner;
        }
        #endregion

        #region Public properties
        public double RadiusOuter
        {
            get { return _radiusOuter; }
            set { _radiusOuter = value; Modify(); }
        }
        public double RadiusInner
        {
            get { return _radiusInner; }
            set { _radiusInner = value; Modify(); }
        }
        public double Height
        {
            get { return _height; }
            set { _height = value; Modify(); }
        }
        public Color ColorTop
        {
            get { return _colorTop; }
            set { _colorTop = value; Modify(); }
        }
        public Color ColorWallOuter
        {
            get { return _colorWallOuter; }
            set { _colorWallOuter = value; Modify(); }
        }
        public Color ColorWallInner
        {
            get { return _colorWallInner; }
            set { _colorWallInner = value; Modify(); }
        }
        #endregion

        #region Public methods

        #endregion

        #region Override packable
        protected override string TypeName
        {
            get { return Properties.Resource.ID_NAMECYLINDER; }
        }
        public override double Volume
        {
            get { return _height * Math.PI * _radiusOuter * _radiusOuter; }
        }
        public override Vector3D OuterDimensions
        {
            get { return new Vector3D(2.0*_radiusOuter, 2.0*_radiusOuter, _height); }
        }
        public override bool IsBrick { get { return false; } }
        #endregion

        #region Object override
        public override string ToString()
        {
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append(base.ToString());
            sBuilder.Append(string.Format("Cylinder => Outer radius = {0} Inner radius = {1} Height = {2} Weight = {3}"
                , _radiusOuter, _radiusInner, _height, _weight) );
            return sBuilder.ToString();
        }
        #endregion
    }
}
