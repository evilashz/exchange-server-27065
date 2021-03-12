using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200104B RID: 4171
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagFswUnableToBindWitnessDirectoryException : LocalizedException
	{
		// Token: 0x0600B034 RID: 45108 RVA: 0x00295960 File Offset: 0x00293B60
		public DagFswUnableToBindWitnessDirectoryException(string host) : base(Strings.DagFswUnableToBindWitnessDirectoryException(host))
		{
			this.host = host;
		}

		// Token: 0x0600B035 RID: 45109 RVA: 0x00295975 File Offset: 0x00293B75
		public DagFswUnableToBindWitnessDirectoryException(string host, Exception innerException) : base(Strings.DagFswUnableToBindWitnessDirectoryException(host), innerException)
		{
			this.host = host;
		}

		// Token: 0x0600B036 RID: 45110 RVA: 0x0029598B File Offset: 0x00293B8B
		protected DagFswUnableToBindWitnessDirectoryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.host = (string)info.GetValue("host", typeof(string));
		}

		// Token: 0x0600B037 RID: 45111 RVA: 0x002959B5 File Offset: 0x00293BB5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("host", this.host);
		}

		// Token: 0x17003825 RID: 14373
		// (get) Token: 0x0600B038 RID: 45112 RVA: 0x002959D0 File Offset: 0x00293BD0
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x0400618B RID: 24971
		private readonly string host;
	}
}
