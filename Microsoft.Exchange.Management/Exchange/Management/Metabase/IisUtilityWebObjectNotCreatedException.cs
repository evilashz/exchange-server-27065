using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000FAC RID: 4012
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IisUtilityWebObjectNotCreatedException : LocalizedException
	{
		// Token: 0x0600AD29 RID: 44329 RVA: 0x00291222 File Offset: 0x0028F422
		public IisUtilityWebObjectNotCreatedException(string parent, string name, string type) : base(Strings.IisUtilityWebObjectNotCreatedException(parent, name, type))
		{
			this.parent = parent;
			this.name = name;
			this.type = type;
		}

		// Token: 0x0600AD2A RID: 44330 RVA: 0x00291247 File Offset: 0x0028F447
		public IisUtilityWebObjectNotCreatedException(string parent, string name, string type, Exception innerException) : base(Strings.IisUtilityWebObjectNotCreatedException(parent, name, type), innerException)
		{
			this.parent = parent;
			this.name = name;
			this.type = type;
		}

		// Token: 0x0600AD2B RID: 44331 RVA: 0x00291270 File Offset: 0x0028F470
		protected IisUtilityWebObjectNotCreatedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.parent = (string)info.GetValue("parent", typeof(string));
			this.name = (string)info.GetValue("name", typeof(string));
			this.type = (string)info.GetValue("type", typeof(string));
		}

		// Token: 0x0600AD2C RID: 44332 RVA: 0x002912E5 File Offset: 0x0028F4E5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("parent", this.parent);
			info.AddValue("name", this.name);
			info.AddValue("type", this.type);
		}

		// Token: 0x17003796 RID: 14230
		// (get) Token: 0x0600AD2D RID: 44333 RVA: 0x00291322 File Offset: 0x0028F522
		public string Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17003797 RID: 14231
		// (get) Token: 0x0600AD2E RID: 44334 RVA: 0x0029132A File Offset: 0x0028F52A
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17003798 RID: 14232
		// (get) Token: 0x0600AD2F RID: 44335 RVA: 0x00291332 File Offset: 0x0028F532
		public string Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x040060FC RID: 24828
		private readonly string parent;

		// Token: 0x040060FD RID: 24829
		private readonly string name;

		// Token: 0x040060FE RID: 24830
		private readonly string type;
	}
}
