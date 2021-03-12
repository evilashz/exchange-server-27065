using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.RightsManagementServices.Online;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x02000744 RID: 1860
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class RmsOnlineImportTpdException : ImportTpdException
	{
		// Token: 0x1700140D RID: 5133
		// (get) Token: 0x060041DD RID: 16861 RVA: 0x0010CCC0 File Offset: 0x0010AEC0
		// (set) Token: 0x060041DE RID: 16862 RVA: 0x0010CCC8 File Offset: 0x0010AEC8
		public ServerErrorCode ServerErrorCode { get; set; }

		// Token: 0x060041DF RID: 16863 RVA: 0x0010CCD1 File Offset: 0x0010AED1
		public RmsOnlineImportTpdException()
		{
		}

		// Token: 0x060041E0 RID: 16864 RVA: 0x0010CCD9 File Offset: 0x0010AED9
		protected RmsOnlineImportTpdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ServerErrorCode = (ServerErrorCode)info.GetInt32("ServerErrorCode");
		}

		// Token: 0x060041E1 RID: 16865 RVA: 0x0010CCF4 File Offset: 0x0010AEF4
		public RmsOnlineImportTpdException(string message, ServerErrorCode serverErrorCode) : base(message, null)
		{
			this.ServerErrorCode = serverErrorCode;
		}

		// Token: 0x060041E2 RID: 16866 RVA: 0x0010CD08 File Offset: 0x0010AF08
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RMS Online error code: ");
			stringBuilder.Append(this.ServerErrorCode);
			stringBuilder.AppendLine();
			stringBuilder.Append(base.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x060041E3 RID: 16867 RVA: 0x0010CD53 File Offset: 0x0010AF53
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			ArgumentValidator.ThrowIfNull("info", info);
			info.AddValue("ServerErrorCode", (int)this.ServerErrorCode);
			base.GetObjectData(info, context);
		}
	}
}
