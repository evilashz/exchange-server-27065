using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001079 RID: 4217
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskComputerAccountExistsAndIsEnabledException : LocalizedException
	{
		// Token: 0x0600B142 RID: 45378 RVA: 0x00297BC6 File Offset: 0x00295DC6
		public DagTaskComputerAccountExistsAndIsEnabledException(string cnoName) : base(Strings.DagTaskComputerAccountExistsAndIsEnabledException(cnoName))
		{
			this.cnoName = cnoName;
		}

		// Token: 0x0600B143 RID: 45379 RVA: 0x00297BDB File Offset: 0x00295DDB
		public DagTaskComputerAccountExistsAndIsEnabledException(string cnoName, Exception innerException) : base(Strings.DagTaskComputerAccountExistsAndIsEnabledException(cnoName), innerException)
		{
			this.cnoName = cnoName;
		}

		// Token: 0x0600B144 RID: 45380 RVA: 0x00297BF1 File Offset: 0x00295DF1
		protected DagTaskComputerAccountExistsAndIsEnabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.cnoName = (string)info.GetValue("cnoName", typeof(string));
		}

		// Token: 0x0600B145 RID: 45381 RVA: 0x00297C1B File Offset: 0x00295E1B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("cnoName", this.cnoName);
		}

		// Token: 0x1700387B RID: 14459
		// (get) Token: 0x0600B146 RID: 45382 RVA: 0x00297C36 File Offset: 0x00295E36
		public string CnoName
		{
			get
			{
				return this.cnoName;
			}
		}

		// Token: 0x040061E1 RID: 25057
		private readonly string cnoName;
	}
}
