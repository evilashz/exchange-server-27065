using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E23 RID: 3619
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RmsSharedIdentityComputerNotFoundException : LocalizedException
	{
		// Token: 0x0600A5B6 RID: 42422 RVA: 0x00286841 File Offset: 0x00284A41
		public RmsSharedIdentityComputerNotFoundException(string serverName) : base(Strings.RmsSharedIdentityComputerNotFound(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600A5B7 RID: 42423 RVA: 0x00286856 File Offset: 0x00284A56
		public RmsSharedIdentityComputerNotFoundException(string serverName, Exception innerException) : base(Strings.RmsSharedIdentityComputerNotFound(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600A5B8 RID: 42424 RVA: 0x0028686C File Offset: 0x00284A6C
		protected RmsSharedIdentityComputerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600A5B9 RID: 42425 RVA: 0x00286896 File Offset: 0x00284A96
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17003647 RID: 13895
		// (get) Token: 0x0600A5BA RID: 42426 RVA: 0x002868B1 File Offset: 0x00284AB1
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04005FAD RID: 24493
		private readonly string serverName;
	}
}
