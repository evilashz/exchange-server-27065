using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x0200121E RID: 4638
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AdamInstallGeneralFailureWithResultException : LocalizedException
	{
		// Token: 0x0600BAF1 RID: 47857 RVA: 0x002A9759 File Offset: 0x002A7959
		public AdamInstallGeneralFailureWithResultException(int adamErrorCode) : base(Strings.AdamInstallGeneralFailureWithResult(adamErrorCode))
		{
			this.adamErrorCode = adamErrorCode;
		}

		// Token: 0x0600BAF2 RID: 47858 RVA: 0x002A976E File Offset: 0x002A796E
		public AdamInstallGeneralFailureWithResultException(int adamErrorCode, Exception innerException) : base(Strings.AdamInstallGeneralFailureWithResult(adamErrorCode), innerException)
		{
			this.adamErrorCode = adamErrorCode;
		}

		// Token: 0x0600BAF3 RID: 47859 RVA: 0x002A9784 File Offset: 0x002A7984
		protected AdamInstallGeneralFailureWithResultException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.adamErrorCode = (int)info.GetValue("adamErrorCode", typeof(int));
		}

		// Token: 0x0600BAF4 RID: 47860 RVA: 0x002A97AE File Offset: 0x002A79AE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("adamErrorCode", this.adamErrorCode);
		}

		// Token: 0x17003AD9 RID: 15065
		// (get) Token: 0x0600BAF5 RID: 47861 RVA: 0x002A97C9 File Offset: 0x002A79C9
		public int AdamErrorCode
		{
			get
			{
				return this.adamErrorCode;
			}
		}

		// Token: 0x04006561 RID: 25953
		private readonly int adamErrorCode;
	}
}
