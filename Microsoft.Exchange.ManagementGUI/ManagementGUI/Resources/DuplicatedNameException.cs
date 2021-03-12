using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.ManagementGUI.Resources
{
	// Token: 0x02000015 RID: 21
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DuplicatedNameException : ArgumentException
	{
		// Token: 0x060010B5 RID: 4277 RVA: 0x00037008 File Offset: 0x00035208
		public DuplicatedNameException(string name) : base(Strings.DuplicatedName(name))
		{
			this.name = name;
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00037022 File Offset: 0x00035222
		public DuplicatedNameException(string name, Exception innerException) : base(Strings.DuplicatedName(name), innerException)
		{
			this.name = name;
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0003703D File Offset: 0x0003523D
		protected DuplicatedNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00037067 File Offset: 0x00035267
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x00037082 File Offset: 0x00035282
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04001071 RID: 4209
		private readonly string name;
	}
}
