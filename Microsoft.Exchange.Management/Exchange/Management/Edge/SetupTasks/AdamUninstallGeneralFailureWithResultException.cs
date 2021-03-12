using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02001220 RID: 4640
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AdamUninstallGeneralFailureWithResultException : LocalizedException
	{
		// Token: 0x0600BAFC RID: 47868 RVA: 0x002A989D File Offset: 0x002A7A9D
		public AdamUninstallGeneralFailureWithResultException(int adamErrorCode) : base(Strings.AdamUninstallGeneralFailureWithResult(adamErrorCode))
		{
			this.adamErrorCode = adamErrorCode;
		}

		// Token: 0x0600BAFD RID: 47869 RVA: 0x002A98B2 File Offset: 0x002A7AB2
		public AdamUninstallGeneralFailureWithResultException(int adamErrorCode, Exception innerException) : base(Strings.AdamUninstallGeneralFailureWithResult(adamErrorCode), innerException)
		{
			this.adamErrorCode = adamErrorCode;
		}

		// Token: 0x0600BAFE RID: 47870 RVA: 0x002A98C8 File Offset: 0x002A7AC8
		protected AdamUninstallGeneralFailureWithResultException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.adamErrorCode = (int)info.GetValue("adamErrorCode", typeof(int));
		}

		// Token: 0x0600BAFF RID: 47871 RVA: 0x002A98F2 File Offset: 0x002A7AF2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("adamErrorCode", this.adamErrorCode);
		}

		// Token: 0x17003ADC RID: 15068
		// (get) Token: 0x0600BB00 RID: 47872 RVA: 0x002A990D File Offset: 0x002A7B0D
		public int AdamErrorCode
		{
			get
			{
				return this.adamErrorCode;
			}
		}

		// Token: 0x04006564 RID: 25956
		private readonly int adamErrorCode;
	}
}
