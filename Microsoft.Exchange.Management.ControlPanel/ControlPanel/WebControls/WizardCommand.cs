using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000637 RID: 1591
	public class WizardCommand : PopupCommand
	{
		// Token: 0x060045ED RID: 17901 RVA: 0x000D37CD File Offset: 0x000D19CD
		public WizardCommand() : this(null, CommandSprite.SpriteId.NONE)
		{
		}

		// Token: 0x060045EE RID: 17902 RVA: 0x000D37D7 File Offset: 0x000D19D7
		public WizardCommand(string text, CommandSprite.SpriteId imageID) : base(text, imageID)
		{
			this.Name = "New";
		}

		// Token: 0x170026F3 RID: 9971
		// (get) Token: 0x060045EF RID: 17903 RVA: 0x000D37F7 File Offset: 0x000D19F7
		// (set) Token: 0x060045F0 RID: 17904 RVA: 0x000D37FF File Offset: 0x000D19FF
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override Size DialogSize
		{
			get
			{
				return base.DialogSize;
			}
			set
			{
				base.DialogSize = value;
			}
		}

		// Token: 0x170026F4 RID: 9972
		// (get) Token: 0x060045F1 RID: 17905 RVA: 0x000D3808 File Offset: 0x000D1A08
		// (set) Token: 0x060045F2 RID: 17906 RVA: 0x000D3810 File Offset: 0x000D1A10
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override bool Resizable
		{
			get
			{
				return base.Resizable;
			}
			set
			{
				base.Resizable = value;
			}
		}

		// Token: 0x170026F5 RID: 9973
		// (get) Token: 0x060045F3 RID: 17907 RVA: 0x000D3819 File Offset: 0x000D1A19
		// (set) Token: 0x060045F4 RID: 17908 RVA: 0x000D3821 File Offset: 0x000D1A21
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override bool UseDefaultWindow
		{
			get
			{
				return base.UseDefaultWindow;
			}
			set
			{
				base.UseDefaultWindow = value;
			}
		}

		// Token: 0x170026F6 RID: 9974
		// (get) Token: 0x060045F5 RID: 17909 RVA: 0x000D382A File Offset: 0x000D1A2A
		// (set) Token: 0x060045F6 RID: 17910 RVA: 0x000D3832 File Offset: 0x000D1A32
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool ShowAddressBar
		{
			get
			{
				return base.ShowAddressBar;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170026F7 RID: 9975
		// (get) Token: 0x060045F7 RID: 17911 RVA: 0x000D3839 File Offset: 0x000D1A39
		// (set) Token: 0x060045F8 RID: 17912 RVA: 0x000D3841 File Offset: 0x000D1A41
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool ShowMenuBar
		{
			get
			{
				return base.ShowMenuBar;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170026F8 RID: 9976
		// (get) Token: 0x060045F9 RID: 17913 RVA: 0x000D3848 File Offset: 0x000D1A48
		// (set) Token: 0x060045FA RID: 17914 RVA: 0x000D3850 File Offset: 0x000D1A50
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool ShowStatusBar
		{
			get
			{
				return base.ShowStatusBar;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170026F9 RID: 9977
		// (get) Token: 0x060045FB RID: 17915 RVA: 0x000D3857 File Offset: 0x000D1A57
		// (set) Token: 0x060045FC RID: 17916 RVA: 0x000D385F File Offset: 0x000D1A5F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool ShowToolBar
		{
			get
			{
				return base.ShowToolBar;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170026FA RID: 9978
		// (get) Token: 0x060045FD RID: 17917 RVA: 0x000D3866 File Offset: 0x000D1A66
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public BindingCollection QueryParameters
		{
			get
			{
				return this.queryParameters;
			}
		}

		// Token: 0x170026FB RID: 9979
		// (get) Token: 0x060045FE RID: 17918 RVA: 0x000D386E File Offset: 0x000D1A6E
		// (set) Token: 0x060045FF RID: 17919 RVA: 0x000D387C File Offset: 0x000D1A7C
		public override string NavigateUrl
		{
			get
			{
				this.UpdateNavigateUrlIfRequired();
				return base.NavigateUrl;
			}
			set
			{
				base.NavigateUrl = value;
			}
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x000D3888 File Offset: 0x000D1A88
		private void UpdateNavigateUrlIfRequired()
		{
			if (!this.isQuertyParametersInitializied && this.QueryParameters.Count > 0)
			{
				string text = base.NavigateUrl;
				if (string.IsNullOrEmpty(text))
				{
					throw new ArgumentException("NavigateUrl must be specified, QueryParameters is used.");
				}
				foreach (Binding binding in this.QueryParameters)
				{
					StaticBinding staticBinding = (StaticBinding)binding;
					if (staticBinding.HasValue || !staticBinding.Optional)
					{
						if (staticBinding.Value is Identity)
						{
							text = EcpUrl.AppendQueryParameter(text, staticBinding.Name, ((Identity)staticBinding.Value).RawIdentity);
						}
						else
						{
							text = EcpUrl.AppendQueryParameter(text, staticBinding.Name, staticBinding.Value.ToString());
						}
					}
				}
				base.NavigateUrl = text;
				this.isQuertyParametersInitializied = true;
			}
		}

		// Token: 0x04002F52 RID: 12114
		private BindingCollection queryParameters = new BindingCollection();

		// Token: 0x04002F53 RID: 12115
		private bool isQuertyParametersInitializied;
	}
}
