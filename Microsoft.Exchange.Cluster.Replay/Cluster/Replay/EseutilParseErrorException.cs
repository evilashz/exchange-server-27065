using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200039A RID: 922
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EseutilParseErrorException : TransientException
	{
		// Token: 0x0600274B RID: 10059 RVA: 0x000B5A91 File Offset: 0x000B3C91
		public EseutilParseErrorException(string line, string regex) : base(ReplayStrings.EseutilParseError(line, regex))
		{
			this.line = line;
			this.regex = regex;
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x000B5AAE File Offset: 0x000B3CAE
		public EseutilParseErrorException(string line, string regex, Exception innerException) : base(ReplayStrings.EseutilParseError(line, regex), innerException)
		{
			this.line = line;
			this.regex = regex;
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x000B5ACC File Offset: 0x000B3CCC
		protected EseutilParseErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.line = (string)info.GetValue("line", typeof(string));
			this.regex = (string)info.GetValue("regex", typeof(string));
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x000B5B21 File Offset: 0x000B3D21
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("line", this.line);
			info.AddValue("regex", this.regex);
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x0600274F RID: 10063 RVA: 0x000B5B4D File Offset: 0x000B3D4D
		public string Line
		{
			get
			{
				return this.line;
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06002750 RID: 10064 RVA: 0x000B5B55 File Offset: 0x000B3D55
		public string Regex
		{
			get
			{
				return this.regex;
			}
		}

		// Token: 0x0400138A RID: 5002
		private readonly string line;

		// Token: 0x0400138B RID: 5003
		private readonly string regex;
	}
}
