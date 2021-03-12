using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200013C RID: 316
	[Serializable]
	public class ReceiveConnectorIdParameter : ServerBasedIdParameter
	{
		// Token: 0x06000B41 RID: 2881 RVA: 0x000240A3 File Offset: 0x000222A3
		public ReceiveConnectorIdParameter()
		{
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x000240AB File Offset: 0x000222AB
		public ReceiveConnectorIdParameter(ADObjectId adObjectId) : base(adObjectId)
		{
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000240B4 File Offset: 0x000222B4
		public ReceiveConnectorIdParameter(ReceiveConnector connector) : base(connector.Id)
		{
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x000240C2 File Offset: 0x000222C2
		public ReceiveConnectorIdParameter(INamedIdentity namedIdentity) : base(namedIdentity)
		{
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x000240CC File Offset: 0x000222CC
		protected ReceiveConnectorIdParameter(string identity) : base(identity)
		{
			string[] array = identity.Split(new char[]
			{
				'\\'
			});
			switch (array.Length)
			{
			case 1:
			case 2:
				foreach (string value in array)
				{
					if (string.IsNullOrEmpty(value))
					{
						throw new ArgumentException(Strings.ErrorInvalidIdentity(identity), "identity");
					}
				}
				return;
			default:
				throw new ArgumentException(Strings.ErrorInvalidIdentity(identity), "identity");
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0002415A File Offset: 0x0002235A
		protected override ServerRole RoleRestriction
		{
			get
			{
				return ServerRole.All;
			}
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00024161 File Offset: 0x00022361
		public static ReceiveConnectorIdParameter Parse(string identity)
		{
			return new ReceiveConnectorIdParameter(identity);
		}

		// Token: 0x04000299 RID: 665
		private const char CommonNameSeperatorChar = '\\';
	}
}
