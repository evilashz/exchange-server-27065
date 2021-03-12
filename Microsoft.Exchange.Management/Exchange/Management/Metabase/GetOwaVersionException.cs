using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000DE1 RID: 3553
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GetOwaVersionException : DataSourceOperationException
	{
		// Token: 0x0600A453 RID: 42067 RVA: 0x00284089 File Offset: 0x00282289
		public GetOwaVersionException(string owaDllPath) : base(Strings.GetOwaVersionException(owaDllPath))
		{
			this.owaDllPath = owaDllPath;
		}

		// Token: 0x0600A454 RID: 42068 RVA: 0x0028409E File Offset: 0x0028229E
		public GetOwaVersionException(string owaDllPath, Exception innerException) : base(Strings.GetOwaVersionException(owaDllPath), innerException)
		{
			this.owaDllPath = owaDllPath;
		}

		// Token: 0x0600A455 RID: 42069 RVA: 0x002840B4 File Offset: 0x002822B4
		protected GetOwaVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.owaDllPath = (string)info.GetValue("owaDllPath", typeof(string));
		}

		// Token: 0x0600A456 RID: 42070 RVA: 0x002840DE File Offset: 0x002822DE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("owaDllPath", this.owaDllPath);
		}

		// Token: 0x170035EC RID: 13804
		// (get) Token: 0x0600A457 RID: 42071 RVA: 0x002840F9 File Offset: 0x002822F9
		public string OwaDllPath
		{
			get
			{
				return this.owaDllPath;
			}
		}

		// Token: 0x04005F52 RID: 24402
		private readonly string owaDllPath;
	}
}
