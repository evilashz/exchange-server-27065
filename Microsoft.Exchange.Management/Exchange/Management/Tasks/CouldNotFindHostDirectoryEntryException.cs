using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DF4 RID: 3572
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotFindHostDirectoryEntryException : LocalizedException
	{
		// Token: 0x0600A4BD RID: 42173 RVA: 0x00284D24 File Offset: 0x00282F24
		public CouldNotFindHostDirectoryEntryException(string host) : base(Strings.CouldNotFindHostDirectoryEntryException(host))
		{
			this.host = host;
		}

		// Token: 0x0600A4BE RID: 42174 RVA: 0x00284D39 File Offset: 0x00282F39
		public CouldNotFindHostDirectoryEntryException(string host, Exception innerException) : base(Strings.CouldNotFindHostDirectoryEntryException(host), innerException)
		{
			this.host = host;
		}

		// Token: 0x0600A4BF RID: 42175 RVA: 0x00284D4F File Offset: 0x00282F4F
		protected CouldNotFindHostDirectoryEntryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.host = (string)info.GetValue("host", typeof(string));
		}

		// Token: 0x0600A4C0 RID: 42176 RVA: 0x00284D79 File Offset: 0x00282F79
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("host", this.host);
		}

		// Token: 0x1700360A RID: 13834
		// (get) Token: 0x0600A4C1 RID: 42177 RVA: 0x00284D94 File Offset: 0x00282F94
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x04005F70 RID: 24432
		private readonly string host;
	}
}
