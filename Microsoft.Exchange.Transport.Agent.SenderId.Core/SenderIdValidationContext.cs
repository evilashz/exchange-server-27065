using System;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x0200000C RID: 12
	internal sealed class SenderIdValidationContext
	{
		// Token: 0x0600002D RID: 45 RVA: 0x0000264E File Offset: 0x0000084E
		internal SenderIdValidationContext(SenderIdValidationBaseContext baseContext, string purportedResponsibleDomain, bool processExpModifier, AsyncCallback asyncCallback, object asyncState)
		{
			this.baseContext = baseContext;
			this.purportedResponsibleDomain = purportedResponsibleDomain.ToLowerInvariant();
			this.processExpModifier = processExpModifier;
			this.clientAR = new SenderIdAsyncResult(asyncCallback, asyncState);
			this.isValid = true;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002686 File Offset: 0x00000886
		public SenderIdValidationBaseContext BaseContext
		{
			get
			{
				return this.baseContext;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000268E File Offset: 0x0000088E
		public string PurportedResponsibleDomain
		{
			get
			{
				return this.purportedResponsibleDomain;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002696 File Offset: 0x00000896
		public bool ProcessExpModifier
		{
			get
			{
				return this.processExpModifier;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000269E File Offset: 0x0000089E
		public ExpSpfModifier Exp
		{
			get
			{
				return this.exp;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000026A6 File Offset: 0x000008A6
		public SenderIdAsyncResult ClientAR
		{
			get
			{
				return this.clientAR;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000026AE File Offset: 0x000008AE
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000026B6 File Offset: 0x000008B6
		public void AddExpModifier(ExpSpfModifier modifier)
		{
			if (this.exp == null)
			{
				this.exp = modifier;
				return;
			}
			this.SetInvalid();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000026CE File Offset: 0x000008CE
		public void ValidationCompleted(SenderIdStatus status)
		{
			this.ValidationCompleted(new SenderIdResult(status));
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000026DC File Offset: 0x000008DC
		public void ValidationCompleted(SenderIdStatus status, SenderIdFailReason failReason)
		{
			this.ValidationCompleted(new SenderIdResult(status, failReason));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000026EB File Offset: 0x000008EB
		public void ValidationCompleted(SenderIdStatus status, SenderIdFailReason failReason, string explanation)
		{
			this.ValidationCompleted(new SenderIdResult(status, failReason, explanation));
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000026FB File Offset: 0x000008FB
		public void ValidationCompleted(SenderIdResult result)
		{
			this.BaseContext.SenderIdValidator.ValidationCompleted(this, result);
			this.clientAR.InvokeCompleted(result);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000271B File Offset: 0x0000091B
		public void SetInvalid()
		{
			this.isValid = false;
		}

		// Token: 0x04000028 RID: 40
		private SenderIdValidationBaseContext baseContext;

		// Token: 0x04000029 RID: 41
		private string purportedResponsibleDomain;

		// Token: 0x0400002A RID: 42
		private bool processExpModifier;

		// Token: 0x0400002B RID: 43
		private ExpSpfModifier exp;

		// Token: 0x0400002C RID: 44
		private SenderIdAsyncResult clientAR;

		// Token: 0x0400002D RID: 45
		private bool isValid;
	}
}
