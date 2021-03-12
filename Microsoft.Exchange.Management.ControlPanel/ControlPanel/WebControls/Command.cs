using System;
using System.ComponentModel;
using System.Security.Principal;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000571 RID: 1393
	public class Command
	{
		// Token: 0x060040CF RID: 16591 RVA: 0x000C6027 File Offset: 0x000C4227
		public Command() : this(null, CommandSprite.SpriteId.NONE)
		{
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x000C6034 File Offset: 0x000C4234
		public Command(string text, CommandSprite.SpriteId imageID)
		{
			this.OnClientClick = base.GetType().Name + "Handler";
			this.SelectionMode = SelectionMode.SelectionIndependent;
			this.RefreshAction = RefreshAction.SingleRow;
			this.ConfirmDialogType = ModalDialogType.Warning;
			this.Text = text;
			this.ImageId = imageID;
			this.Visible = true;
			this.CustomSpriteCss = string.Empty;
		}

		// Token: 0x17002524 RID: 9508
		// (get) Token: 0x060040D1 RID: 16593 RVA: 0x000C6097 File Offset: 0x000C4297
		// (set) Token: 0x060040D2 RID: 16594 RVA: 0x000C609F File Offset: 0x000C429F
		[DefaultValue("")]
		public string Condition { get; set; }

		// Token: 0x17002525 RID: 9509
		// (get) Token: 0x060040D3 RID: 16595 RVA: 0x000C60A8 File Offset: 0x000C42A8
		// (set) Token: 0x060040D4 RID: 16596 RVA: 0x000C60B0 File Offset: 0x000C42B0
		[DefaultValue("")]
		public string GroupId { get; set; }

		// Token: 0x17002526 RID: 9510
		// (get) Token: 0x060040D5 RID: 16597 RVA: 0x000C60B9 File Offset: 0x000C42B9
		// (set) Token: 0x060040D6 RID: 16598 RVA: 0x000C60C1 File Offset: 0x000C42C1
		[Localizable(true)]
		[DefaultValue("")]
		public string SingleSelectionConfirmMessage { get; set; }

		// Token: 0x17002527 RID: 9511
		// (get) Token: 0x060040D7 RID: 16599 RVA: 0x000C60CA File Offset: 0x000C42CA
		// (set) Token: 0x060040D8 RID: 16600 RVA: 0x000C60D2 File Offset: 0x000C42D2
		[DefaultValue("")]
		[Localizable(true)]
		public string MultiSelectionConfirmMessage { get; set; }

		// Token: 0x17002528 RID: 9512
		// (get) Token: 0x060040D9 RID: 16601 RVA: 0x000C60DB File Offset: 0x000C42DB
		// (set) Token: 0x060040DA RID: 16602 RVA: 0x000C60E3 File Offset: 0x000C42E3
		[DefaultValue("")]
		[Localizable(true)]
		public string SelectionConfirmMessageDetail { get; set; }

		// Token: 0x17002529 RID: 9513
		// (get) Token: 0x060040DB RID: 16603 RVA: 0x000C60EC File Offset: 0x000C42EC
		// (set) Token: 0x060040DC RID: 16604 RVA: 0x000C60F4 File Offset: 0x000C42F4
		[DefaultValue(false)]
		public virtual bool DefaultCommand { get; set; }

		// Token: 0x1700252A RID: 9514
		// (get) Token: 0x060040DD RID: 16605 RVA: 0x000C60FD File Offset: 0x000C42FD
		// (set) Token: 0x060040DE RID: 16606 RVA: 0x000C6105 File Offset: 0x000C4305
		[DefaultValue("")]
		public string ShortCut
		{
			get
			{
				return this.shortCut;
			}
			set
			{
				this.shortCut = value;
				this.CoerceRefreshAction();
			}
		}

		// Token: 0x1700252B RID: 9515
		// (get) Token: 0x060040DF RID: 16607 RVA: 0x000C6114 File Offset: 0x000C4314
		// (set) Token: 0x060040E0 RID: 16608 RVA: 0x000C611C File Offset: 0x000C431C
		[DefaultValue(RefreshAction.SingleRow)]
		public RefreshAction RefreshAction { get; set; }

		// Token: 0x060040E1 RID: 16609 RVA: 0x000C6125 File Offset: 0x000C4325
		private void CoerceRefreshAction()
		{
			if (this.ShortCut == "Delete" && this.RefreshAction == RefreshAction.SingleRow)
			{
				this.RefreshAction = RefreshAction.RemoveSingleRow;
			}
		}

		// Token: 0x1700252C RID: 9516
		// (get) Token: 0x060040E2 RID: 16610 RVA: 0x000C6148 File Offset: 0x000C4348
		// (set) Token: 0x060040E3 RID: 16611 RVA: 0x000C6150 File Offset: 0x000C4350
		[DefaultValue(false)]
		public bool HideOnDisable { get; set; }

		// Token: 0x1700252D RID: 9517
		// (get) Token: 0x060040E4 RID: 16612 RVA: 0x000C6159 File Offset: 0x000C4359
		// (set) Token: 0x060040E5 RID: 16613 RVA: 0x000C6161 File Offset: 0x000C4361
		[DefaultValue(true)]
		public bool Visible { get; set; }

		// Token: 0x1700252E RID: 9518
		// (get) Token: 0x060040E6 RID: 16614 RVA: 0x000C616A File Offset: 0x000C436A
		// (set) Token: 0x060040E7 RID: 16615 RVA: 0x000C6172 File Offset: 0x000C4372
		[Bindable(true)]
		public CommandSprite.SpriteId ImageId { get; set; }

		// Token: 0x1700252F RID: 9519
		// (get) Token: 0x060040E8 RID: 16616 RVA: 0x000C617B File Offset: 0x000C437B
		// (set) Token: 0x060040E9 RID: 16617 RVA: 0x000C6183 File Offset: 0x000C4383
		[Bindable(true)]
		public string CustomSpriteCss { get; set; }

		// Token: 0x17002530 RID: 9520
		// (get) Token: 0x060040EA RID: 16618 RVA: 0x000C618C File Offset: 0x000C438C
		// (set) Token: 0x060040EB RID: 16619 RVA: 0x000C6194 File Offset: 0x000C4394
		[Bindable(false)]
		[DefaultValue("")]
		public virtual string Name { get; set; }

		// Token: 0x17002531 RID: 9521
		// (get) Token: 0x060040EC RID: 16620 RVA: 0x000C619D File Offset: 0x000C439D
		// (set) Token: 0x060040ED RID: 16621 RVA: 0x000C61A5 File Offset: 0x000C43A5
		public virtual string OnClientClick { get; set; }

		// Token: 0x17002532 RID: 9522
		// (get) Token: 0x060040EE RID: 16622 RVA: 0x000C61AE File Offset: 0x000C43AE
		// (set) Token: 0x060040EF RID: 16623 RVA: 0x000C61CF File Offset: 0x000C43CF
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] Roles
		{
			get
			{
				if (this.roles == null)
				{
					return new string[0];
				}
				return (string[])this.roles.Clone();
			}
			set
			{
				if (value == null)
				{
					this.roles = value;
					return;
				}
				this.roles = (string[])value.Clone();
			}
		}

		// Token: 0x17002533 RID: 9523
		// (get) Token: 0x060040F0 RID: 16624 RVA: 0x000C61ED File Offset: 0x000C43ED
		// (set) Token: 0x060040F1 RID: 16625 RVA: 0x000C61F5 File Offset: 0x000C43F5
		[DefaultValue(SelectionMode.SelectionIndependent)]
		public virtual SelectionMode SelectionMode { get; set; }

		// Token: 0x17002534 RID: 9524
		// (get) Token: 0x060040F2 RID: 16626 RVA: 0x000C61FE File Offset: 0x000C43FE
		// (set) Token: 0x060040F3 RID: 16627 RVA: 0x000C6206 File Offset: 0x000C4406
		[DefaultValue(false)]
		public virtual bool UseCustomConfirmDialog { get; set; }

		// Token: 0x17002535 RID: 9525
		// (get) Token: 0x060040F4 RID: 16628 RVA: 0x000C620F File Offset: 0x000C440F
		// (set) Token: 0x060040F5 RID: 16629 RVA: 0x000C6217 File Offset: 0x000C4417
		[DefaultValue(ModalDialogType.Warning)]
		public ModalDialogType ConfirmDialogType { get; set; }

		// Token: 0x17002536 RID: 9526
		// (get) Token: 0x060040F6 RID: 16630 RVA: 0x000C6220 File Offset: 0x000C4420
		// (set) Token: 0x060040F7 RID: 16631 RVA: 0x000C6228 File Offset: 0x000C4428
		[DefaultValue("")]
		[Localizable(true)]
		public string Text { get; set; }

		// Token: 0x17002537 RID: 9527
		// (get) Token: 0x060040F8 RID: 16632 RVA: 0x000C6231 File Offset: 0x000C4431
		// (set) Token: 0x060040F9 RID: 16633 RVA: 0x000C6239 File Offset: 0x000C4439
		[Localizable(true)]
		[DefaultValue("")]
		public string Description { get; set; }

		// Token: 0x17002538 RID: 9528
		// (get) Token: 0x060040FA RID: 16634 RVA: 0x000C6242 File Offset: 0x000C4442
		// (set) Token: 0x060040FB RID: 16635 RVA: 0x000C624A File Offset: 0x000C444A
		[DefaultValue("false")]
		public bool AsMoreOption { get; set; }

		// Token: 0x17002539 RID: 9529
		// (get) Token: 0x060040FC RID: 16636 RVA: 0x000C6253 File Offset: 0x000C4453
		// (set) Token: 0x060040FD RID: 16637 RVA: 0x000C625B File Offset: 0x000C445B
		public string ClientCommandHandler { get; set; }

		// Token: 0x1700253A RID: 9530
		// (get) Token: 0x060040FE RID: 16638 RVA: 0x000C6264 File Offset: 0x000C4464
		// (set) Token: 0x060040FF RID: 16639 RVA: 0x000C626C File Offset: 0x000C446C
		[Localizable(true)]
		[DefaultValue("")]
		public string ImageAltText { get; set; }

		// Token: 0x06004100 RID: 16640 RVA: 0x000C6275 File Offset: 0x000C4475
		public virtual bool IsAccessibleToUser(IPrincipal user)
		{
			return this.roles == null || LoginUtil.IsInRoles(user, this.roles);
		}

		// Token: 0x06004101 RID: 16641 RVA: 0x000C628D File Offset: 0x000C448D
		protected internal virtual void PreRender(Control c)
		{
		}

		// Token: 0x04002B0C RID: 11020
		private string[] roles;

		// Token: 0x04002B0D RID: 11021
		private string shortCut;
	}
}
