using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000291 RID: 657
	public class WebReadyViewBody : OwaPage
	{
		// Token: 0x06001930 RID: 6448 RVA: 0x00092CB4 File Offset: 0x00090EB4
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.utilities = new WebReadyViewUtilities(base.OwaContext);
			this.utilities.InvokeTaskManager();
			this.previousButtonState = (this.utilities.CurrentPageNumber != 1);
			this.nextButtonState = (this.utilities.CurrentPageNumber != this.utilities.TotalPageNumber);
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001931 RID: 6449 RVA: 0x00092D1C File Offset: 0x00090F1C
		protected bool PreviousButtonState
		{
			get
			{
				return this.previousButtonState;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001932 RID: 6450 RVA: 0x00092D24 File Offset: 0x00090F24
		protected bool NextButtonState
		{
			get
			{
				return this.nextButtonState;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x00092D2C File Offset: 0x00090F2C
		protected bool IsSupportPaging
		{
			get
			{
				return this.utilities.IsSupportPaging;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001934 RID: 6452 RVA: 0x00092D39 File Offset: 0x00090F39
		protected bool IsCopyRestricted
		{
			get
			{
				return !this.utilities.HasError && this.utilities.IsCopyRestricted;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x00092D55 File Offset: 0x00090F55
		protected bool IsPrintRestricted
		{
			get
			{
				return !this.utilities.HasError && this.utilities.IsPrintRestricted;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001936 RID: 6454 RVA: 0x00092D71 File Offset: 0x00090F71
		protected bool IsRestricted
		{
			get
			{
				return !this.utilities.HasError && this.utilities.IsIrmProtected;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001937 RID: 6455 RVA: 0x00092D8D File Offset: 0x00090F8D
		protected SanitizedHtmlString IrmTemplateDescription
		{
			get
			{
				return this.utilities.IrmInfobarMessage;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x00092D9A File Offset: 0x00090F9A
		protected bool HasError
		{
			get
			{
				return this.utilities.HasError;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x00092DA7 File Offset: 0x00090FA7
		public string ErrorMessage
		{
			get
			{
				return this.utilities.ErrorMessage;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x0600193A RID: 6458 RVA: 0x00092DB4 File Offset: 0x00090FB4
		protected int CurrentPageNumber
		{
			get
			{
				return this.utilities.CurrentPageNumber;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x0600193B RID: 6459 RVA: 0x00092DC1 File Offset: 0x00090FC1
		protected int TotalPageNumber
		{
			get
			{
				return this.utilities.TotalPageNumber;
			}
		}

		// Token: 0x04001267 RID: 4711
		private WebReadyViewUtilities utilities;

		// Token: 0x04001268 RID: 4712
		private bool previousButtonState;

		// Token: 0x04001269 RID: 4713
		private bool nextButtonState;
	}
}
