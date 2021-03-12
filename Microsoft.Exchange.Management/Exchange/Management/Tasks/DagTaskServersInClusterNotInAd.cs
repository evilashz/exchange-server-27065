using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200108A RID: 4234
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskServersInClusterNotInAd : LocalizedException
	{
		// Token: 0x0600B1A3 RID: 45475 RVA: 0x002987A1 File Offset: 0x002969A1
		public DagTaskServersInClusterNotInAd(string serverNames) : base(Strings.DagTaskServersInClusterNotInAd(serverNames))
		{
			this.serverNames = serverNames;
		}

		// Token: 0x0600B1A4 RID: 45476 RVA: 0x002987B6 File Offset: 0x002969B6
		public DagTaskServersInClusterNotInAd(string serverNames, Exception innerException) : base(Strings.DagTaskServersInClusterNotInAd(serverNames), innerException)
		{
			this.serverNames = serverNames;
		}

		// Token: 0x0600B1A5 RID: 45477 RVA: 0x002987CC File Offset: 0x002969CC
		protected DagTaskServersInClusterNotInAd(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverNames = (string)info.GetValue("serverNames", typeof(string));
		}

		// Token: 0x0600B1A6 RID: 45478 RVA: 0x002987F6 File Offset: 0x002969F6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverNames", this.serverNames);
		}

		// Token: 0x17003898 RID: 14488
		// (get) Token: 0x0600B1A7 RID: 45479 RVA: 0x00298811 File Offset: 0x00296A11
		public string ServerNames
		{
			get
			{
				return this.serverNames;
			}
		}

		// Token: 0x040061FE RID: 25086
		private readonly string serverNames;
	}
}
