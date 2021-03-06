﻿#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace treeDiM.StackBuilder.Basics
{
    /// <summary>
    /// Gathers a set of constraint to be used while computing solutions
    /// </summary>
    public abstract class PalletConstraintSet
    {
        #region Data members
        private int _maxNumberOfItems;
        private double _maximumPalletWeight;
        private double _maximumHeight;
        private double _overhangX, _overhangY;
        private bool _useMaximumPalletWeight;
        private bool _useMaximumHeight;
        private bool _useMaximumNumberOfItems;
        private bool _allowAlternateLayers = true;
        private bool _allowAlignedLayers = false;
        private System.Collections.Specialized.StringCollection _allowedPatterns = new System.Collections.Specialized.StringCollection();
        private bool _useNoSolutionsKept;
        private int _noSolutionsKept;
        private int _noPalletFilmTurns = 1;
        #endregion

        #region Constructor
        public PalletConstraintSet()
        {
        }
        #endregion

        #region Validity
        public bool IsValid
        {
            get
            {
                bool hasValidStopCriterion =
                   _useMaximumNumberOfItems
                || _useMaximumHeight
                || UseMaximumWeightOnBox
                || _useMaximumPalletWeight;

                bool allowsAtLeastOneOrthoAxis = false;
                for (int i=0; i<6; ++i)
                {
                    if (AllowOrthoAxis((HalfAxis.HAxis)i))
                    {
                        allowsAtLeastOneOrthoAxis = true;
                        break;
                    }
                }

                return hasValidStopCriterion
                    && allowsAtLeastOneOrthoAxis
                    && (_allowAlignedLayers || _allowAlternateLayers)
                    && _allowedPatterns.Count > 0
                    && (!_useNoSolutionsKept || _noSolutionsKept > 0);
            }
        }
        #endregion

        #region Allow layers aligned / alternate
        public bool AllowAlignedLayers
        {
            set { _allowAlignedLayers = value;  }
            get { return _allowAlignedLayers;   }
        }
        public bool AllowAlternateLayers
        {
            set { _allowAlternateLayers = value;  }
            get { return _allowAlternateLayers; }
        }
        #endregion

        #region Stop conditions
        public bool UseMaximumNumberOfCases
        {
            set { _useMaximumNumberOfItems = value; }
            get { return _useMaximumNumberOfItems; }
        }
        public bool UseMaximumHeight
        {
            set { _useMaximumHeight = value; }
            get { return _useMaximumHeight; }
        }
        public bool UseMaximumPalletWeight
        {
            set { _useMaximumPalletWeight = value; }
            get { return _useMaximumPalletWeight; }
        }
        abstract public bool UseMaximumWeightOnBox { get; set; }
 
        public int MaximumNumberOfItems
        {
            set { _maxNumberOfItems = value; }
            get { return _maxNumberOfItems; }
        }
        public double MaximumPalletWeight
        {
            set { _maximumPalletWeight = value;  }
            get { return _maximumPalletWeight; }
        }
        public double MaximumHeight
        {
            set { _maximumHeight = value; }
            get { return _maximumHeight; }
        }

        abstract public double MaximumWeightOnBox { get; set; }

        #endregion

        #region Allowed patterns
        public void ClearAllowedPatterns()
        {
            _allowedPatterns.Clear();
        }
        public void SetAllowedPattern(string patternName)
        {
            if ( patternName == string.Empty 
                || _allowedPatterns.Contains(patternName) )  return;
            _allowedPatterns.Add(patternName);
        }
        public bool AllowPattern(string patternName)
        {
            if ((string.Equals(patternName, "Symetric Interlocked", StringComparison.CurrentCultureIgnoreCase))
            || (string.Equals(patternName, "Interlocked Filled", StringComparison.CurrentCultureIgnoreCase)))
                return _allowedPatterns.Contains("Interlocked");
            else
                return _allowedPatterns.Contains(patternName);
        }
        public string AllowedPatternString
        {
            set
            {
                string[] patternNames = value.Split(',');
                foreach (string patternName in patternNames)
                    SetAllowedPattern(patternName);
            }
            get
            {
                string sGlobal = string.Empty;
                foreach (string patternName in _allowedPatterns)
                {
                    if (!string.IsNullOrEmpty(sGlobal) )
                        sGlobal += ",";
                    sGlobal += patternName;
                }
                return sGlobal;
            }
        }
        #endregion

        #region Allowed box axis
        abstract public bool AllowOrthoAxis(HalfAxis.HAxis orthoAxis);
        public string AllowOrthoAxisString
        {
            get
            {
                string sGlobal = string.Empty;
                for (int i=0; i<6; ++i)
                {
                    HalfAxis.HAxis axis = (HalfAxis.HAxis)i;
                    if (AllowOrthoAxis(axis))
                    {
                        if (!string.IsNullOrEmpty(sGlobal))
                            sGlobal += ",";
                        sGlobal += HalfAxis.ToString(axis);
                    }
                }
                return sGlobal;
            }
        }

        abstract public bool AllowTwoLayerOrientations { get; set; }
        abstract public bool AllowLastLayerOrientationChange { get; set; }
        #endregion

        #region Overhang / underhang
        public double OverhangX
        {
            get { return _overhangX; }
            set { _overhangX = value; }
        }
        public double OverhangY
        {
            get { return _overhangY; }
            set { _overhangY = value; }
        }
        #endregion

        #region Interlayer
        abstract public bool HasInterlayer { get; set; }
        abstract public int InterlayerPeriod { get; set; }
        abstract public bool HasInterlayerAntiSlip { get; set; }
        #endregion

        #region Number of solutions kept
        public bool UseNumberOfSolutionsKept
        {
            set { _useNoSolutionsKept = value; }
            get { return _useNoSolutionsKept; }
        }
        public int NumberOfSolutionsKept
        {
            set
            {
                _useNoSolutionsKept = true;
                _noSolutionsKept = value;
            }
            get { return _noSolutionsKept; }
        }
        #endregion

        #region Pallet films
        public int PalletFilmTurns
        {
            get { return _noPalletFilmTurns; }
            set { _noPalletFilmTurns = value; }
        }
        #endregion

        #region Allow new layer / allow new box
        public bool AllowNewLayer(int iNoLayer)
        {
            return !UseMaximumWeightOnBox;
        }
        public bool AllowNewBox(int iNoBox)
        {
            return !_useMaximumNumberOfItems || (iNoBox <= _maxNumberOfItems);
        }
        #endregion

        #region Object method override
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (_useMaximumHeight) sb.AppendLine(string.Format("Maximum height = {0}", _maximumHeight));
            if (_useMaximumPalletWeight) sb.AppendLine(string.Format("Maximum pallet weight = {0}", _maximumPalletWeight));
            if (UseMaximumWeightOnBox) sb.AppendLine(string.Format("Maximum weight on box = {0}", 0.0));
            if (_useMaximumNumberOfItems) sb.AppendLine(string.Format("Maximum number of items = {0}", _maxNumberOfItems));
            return sb.ToString();
        }
        #endregion
    }
}
