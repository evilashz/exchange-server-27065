using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration.WebService
{
	// Token: 0x02000372 RID: 882
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "RestoreUserNotAllowedException", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration.WebService")]
	public class RestoreUserNotAllowedException : DataOperationException
	{
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x0008BED5 File Offset: 0x0008A0D5
		// (set) Token: 0x06001643 RID: 5699 RVA: 0x0008BEDD File Offset: 0x0008A0DD
		[DataMember]
		public bool SoftDeletedUserIsTooOld
		{
			get
			{
				return this.SoftDeletedUserIsTooOldField;
			}
			set
			{
				this.SoftDeletedUserIsTooOldField = value;
			}
		}

		// Token: 0x04001003 RID: 4099
		private bool SoftDeletedUserIsTooOldField;
	}
}
