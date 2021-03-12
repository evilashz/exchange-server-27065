using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000FAD RID: 4013
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IsapiFilterNotRemovedException : LocalizedException
	{
		// Token: 0x0600AD30 RID: 44336 RVA: 0x0029133A File Offset: 0x0028F53A
		public IsapiFilterNotRemovedException(string parent, string name) : base(Strings.IsapiFilterNotRemovedException(parent, name))
		{
			this.parent = parent;
			this.name = name;
		}

		// Token: 0x0600AD31 RID: 44337 RVA: 0x00291357 File Offset: 0x0028F557
		public IsapiFilterNotRemovedException(string parent, string name, Exception innerException) : base(Strings.IsapiFilterNotRemovedException(parent, name), innerException)
		{
			this.parent = parent;
			this.name = name;
		}

		// Token: 0x0600AD32 RID: 44338 RVA: 0x00291378 File Offset: 0x0028F578
		protected IsapiFilterNotRemovedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.parent = (string)info.GetValue("parent", typeof(string));
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600AD33 RID: 44339 RVA: 0x002913CD File Offset: 0x0028F5CD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("parent", this.parent);
			info.AddValue("name", this.name);
		}

		// Token: 0x17003799 RID: 14233
		// (get) Token: 0x0600AD34 RID: 44340 RVA: 0x002913F9 File Offset: 0x0028F5F9
		public string Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x1700379A RID: 14234
		// (get) Token: 0x0600AD35 RID: 44341 RVA: 0x00291401 File Offset: 0x0028F601
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040060FF RID: 24831
		private readonly string parent;

		// Token: 0x04006100 RID: 24832
		private readonly string name;
	}
}
