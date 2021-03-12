using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002B4 RID: 692
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServiceStartFailureException : LocalizedException
	{
		// Token: 0x060018F5 RID: 6389 RVA: 0x0005C832 File Offset: 0x0005AA32
		public ServiceStartFailureException(string name, string msg) : base(Strings.ServiceStartFailure(name, msg))
		{
			this.name = name;
			this.msg = msg;
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x0005C84F File Offset: 0x0005AA4F
		public ServiceStartFailureException(string name, string msg, Exception innerException) : base(Strings.ServiceStartFailure(name, msg), innerException)
		{
			this.name = name;
			this.msg = msg;
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x0005C870 File Offset: 0x0005AA70
		protected ServiceStartFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x0005C8C5 File Offset: 0x0005AAC5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x0005C8F1 File Offset: 0x0005AAF1
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060018FA RID: 6394 RVA: 0x0005C8F9 File Offset: 0x0005AAF9
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x0400098A RID: 2442
		private readonly string name;

		// Token: 0x0400098B RID: 2443
		private readonly string msg;
	}
}
