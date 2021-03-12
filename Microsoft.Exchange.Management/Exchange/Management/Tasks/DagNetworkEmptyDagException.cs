using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001071 RID: 4209
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagNetworkEmptyDagException : LocalizedException
	{
		// Token: 0x0600B116 RID: 45334 RVA: 0x002976BD File Offset: 0x002958BD
		public DagNetworkEmptyDagException(string dagName) : base(Strings.DagNetworkEmptyDagException(dagName))
		{
			this.dagName = dagName;
		}

		// Token: 0x0600B117 RID: 45335 RVA: 0x002976D2 File Offset: 0x002958D2
		public DagNetworkEmptyDagException(string dagName, Exception innerException) : base(Strings.DagNetworkEmptyDagException(dagName), innerException)
		{
			this.dagName = dagName;
		}

		// Token: 0x0600B118 RID: 45336 RVA: 0x002976E8 File Offset: 0x002958E8
		protected DagNetworkEmptyDagException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dagName = (string)info.GetValue("dagName", typeof(string));
		}

		// Token: 0x0600B119 RID: 45337 RVA: 0x00297712 File Offset: 0x00295912
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dagName", this.dagName);
		}

		// Token: 0x1700386F RID: 14447
		// (get) Token: 0x0600B11A RID: 45338 RVA: 0x0029772D File Offset: 0x0029592D
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x040061D5 RID: 25045
		private readonly string dagName;
	}
}
