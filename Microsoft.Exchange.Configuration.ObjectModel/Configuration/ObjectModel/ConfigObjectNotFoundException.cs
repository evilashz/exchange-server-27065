using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x020002A4 RID: 676
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigObjectNotFoundException : LocalizedException
	{
		// Token: 0x060018A7 RID: 6311 RVA: 0x0005C134 File Offset: 0x0005A334
		public ConfigObjectNotFoundException(string identity, Type classType) : base(Strings.ConfigObjectNotFound(identity, classType))
		{
			this.identity = identity;
			this.classType = classType;
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x0005C151 File Offset: 0x0005A351
		public ConfigObjectNotFoundException(string identity, Type classType, Exception innerException) : base(Strings.ConfigObjectNotFound(identity, classType), innerException)
		{
			this.identity = identity;
			this.classType = classType;
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x0005C170 File Offset: 0x0005A370
		protected ConfigObjectNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
			this.classType = (Type)info.GetValue("classType", typeof(Type));
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0005C1C5 File Offset: 0x0005A3C5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
			info.AddValue("classType", this.classType);
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x0005C1F1 File Offset: 0x0005A3F1
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x0005C1F9 File Offset: 0x0005A3F9
		public Type ClassType
		{
			get
			{
				return this.classType;
			}
		}

		// Token: 0x0400097C RID: 2428
		private readonly string identity;

		// Token: 0x0400097D RID: 2429
		private readonly Type classType;
	}
}
