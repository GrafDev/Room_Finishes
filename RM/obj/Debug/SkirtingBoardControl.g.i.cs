﻿#pragma checksum "..\..\SkirtingBoardControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5F9BB53C8D151A20C3AE02C08210CC5748F9EECFAE780413B29D4F93D8EE0BE9"
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


namespace RoomFinishes {
    
    
    /// <summary>
    /// SkirtingBoardControl
    /// </summary>
    public partial class WallDialogBox : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\SkirtingBoardControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton all_rooms_radio;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\SkirtingBoardControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton selected_rooms_radio;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\SkirtingBoardControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label board_height_label;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\SkirtingBoardControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Height_TextBox;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\SkirtingBoardControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label select_wall_label;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\SkirtingBoardControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox WallTypeListBox;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\SkirtingBoardControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox join_checkbox;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\SkirtingBoardControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label join_checkbox_label;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\SkirtingBoardControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Ok_Button;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\SkirtingBoardControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Cancel_Button;
        
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
            System.Uri resourceLocater = new System.Uri("/RM;component/skirtingboardcontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SkirtingBoardControl.xaml"
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
            this.all_rooms_radio = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 2:
            this.selected_rooms_radio = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 3:
            this.board_height_label = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.Height_TextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 34 "..\..\SkirtingBoardControl.xaml"
            this.Height_TextBox.LostFocus += new System.Windows.RoutedEventHandler(this.Height_TextBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 5:
            this.select_wall_label = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.WallTypeListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 7:
            this.join_checkbox = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 8:
            this.join_checkbox_label = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.Ok_Button = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\SkirtingBoardControl.xaml"
            this.Ok_Button.Click += new System.Windows.RoutedEventHandler(this.Ok_Button_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.Cancel_Button = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\SkirtingBoardControl.xaml"
            this.Cancel_Button.Click += new System.Windows.RoutedEventHandler(this.Cancel_Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
