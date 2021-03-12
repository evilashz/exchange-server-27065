using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000A9D RID: 2717
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DefaultRoutingGroupNotFoundException : MandatoryContainerNotFoundException
	{
		// Token: 0x06007FDE RID: 32734 RVA: 0x001A493B File Offset: 0x001A2B3B
		public DefaultRoutingGroupNotFoundException(string rgName) : base(DirectoryStrings.DefaultRoutingGroupNotFoundException(rgName))
		{
			this.rgName = rgName;
		}

		// Token: 0x06007FDF RID: 32735 RVA: 0x001A4950 File Offset: 0x001A2B50
		public DefaultRoutingGroupNotFoundException(string rgName, Exception innerException) : base(DirectoryStrings.DefaultRoutingGroupNotFoundException(rgName), innerException)
		{
			this.rgName = rgName;
		}

		// Token: 0x06007FE0 RID: 32736 RVA: 0x001A4966 File Offset: 0x001A2B66
		protected DefaultRoutingGroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.rgName = (string)info.GetValue("rgName", typeof(string));
		}

		// Token: 0x06007FE1 RID: 32737 RVA: 0x001A4990 File Offset: 0x001A2B90
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("rgName", this.rgName);
		}

		// Token: 0x17002EC1 RID: 11969
		// (get) Token: 0x06007FE2 RID: 32738 RVA: 0x001A49AB File Offset: 0x001A2BAB
		public string RgName
		{
			get
			{
				return this.rgName;
			}
		}

		// Token: 0x0400559B RID: 21915
		private readonly string rgName;
	}
}
