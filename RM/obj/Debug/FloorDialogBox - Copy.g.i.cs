﻿#pragma checksum "..\..\FloorDialogBox - Copy.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C61B0C89656F8DBE294D37150E9A218E7DEB42DADC5EE0A9A97C11E47E80681D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using UIFramework.PropertyGrid;


namespace RM {
    
    
    /// <summary>
    /// FloorsControls
    /// </summary>
    public partial class FloorsControls : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\FloorDialogBox - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton allRoomsRadio;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\FloorDialogBox - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton selectRoomRadio;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\FloorDialogBox - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label selectFoorLabel;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\FloorDialogBox - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox FloorTypeListBox;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\FloorDialogBox - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox groupboxParam;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\FloorDialogBox - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox paramSelector;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\FloorDialogBox - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Height_TextBox;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\FloorDialogBox - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button okButton;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\FloorDialogBox - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cancelButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/RM;component/floordialogbox%20-%20copy.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\FloorDialogBox - Copy.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.allRoomsRadio = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 2:
            this.selectRoomRadio = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 3:
            this.selectFoorLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.FloorTypeListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            this.groupboxParam = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 6:
            this.paramSelector = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.Height_TextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 51 "..\..\FloorDialogBox - Copy.xaml"
            this.Height_TextBox.LostFocus += new System.Windows.RoutedEventHandler(this.Height_TextBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 8:
            this.okButton = ((System.Windows.Controls.Button)(target));
            
            #line 60 "..\..\FloorDialogBox - Copy.xaml"
            this.okButton.Click += new System.Windows.RoutedEventHandler(this.Ok_Button_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.cancelButton = ((System.Windows.Controls.Button)(target));
            
            #line 65 "..\..\FloorDialogBox - Copy.xaml"
            this.cancelButton.Click += new System.Windows.RoutedEventHandler(this.Cancel_Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

