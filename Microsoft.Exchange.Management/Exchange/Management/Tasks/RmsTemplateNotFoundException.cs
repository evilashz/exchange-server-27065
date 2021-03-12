using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010F6 RID: 4342
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RmsTemplateNotFoundException : LocalizedException
	{
		// Token: 0x0600B3B8 RID: 46008 RVA: 0x0029B9DF File Offset: 0x00299BDF
		public RmsTemplateNotFoundException(string name) : base(Strings.RmsTemplateNotFound(name))
		{
			this.name = name;
		}

		// Token: 0x0600B3B9 RID: 46009 RVA: 0x0029B9F4 File Offset: 0x00299BF4
		public RmsTemplateNotFoundException(string name, Exception innerException) : base(Strings.RmsTemplateNotFound(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x0600B3BA RID: 46010 RVA: 0x0029BA0A File Offset: 0x00299C0A
		protected RmsTemplateNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x0600B3BB RID: 46011 RVA: 0x0029BA34 File Offset: 0x00299C34
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x170038FD RID: 14589
		// (get) Token: 0x0600B3BC RID: 46012 RVA: 0x0029BA4F File Offset: 0x00299C4F
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04006263 RID: 25187
		private readonly string name;
	}
}
