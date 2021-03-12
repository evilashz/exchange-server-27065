using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200105B RID: 4187
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskClusterServiceNotRunningOnNodeException : LocalizedException
	{
		// Token: 0x0600B098 RID: 45208 RVA: 0x00296739 File Offset: 0x00294939
		public DagTaskClusterServiceNotRunningOnNodeException(string nodeName) : base(Strings.DagTaskClusterServiceNotRunningOnNodeException(nodeName))
		{
			this.nodeName = nodeName;
		}

		// Token: 0x0600B099 RID: 45209 RVA: 0x0029674E File Offset: 0x0029494E
		public DagTaskClusterServiceNotRunningOnNodeException(string nodeName, Exception innerException) : base(Strings.DagTaskClusterServiceNotRunningOnNodeException(nodeName), innerException)
		{
			this.nodeName = nodeName;
		}

		// Token: 0x0600B09A RID: 45210 RVA: 0x00296764 File Offset: 0x00294964
		protected DagTaskClusterServiceNotRunningOnNodeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
		}

		// Token: 0x0600B09B RID: 45211 RVA: 0x0029678E File Offset: 0x0029498E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
		}

		// Token: 0x17003849 RID: 14409
		// (get) Token: 0x0600B09C RID: 45212 RVA: 0x002967A9 File Offset: 0x002949A9
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x040061AF RID: 25007
		private readonly string nodeName;
	}
}
