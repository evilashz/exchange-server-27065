using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x020002A5 RID: 677
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmbiguousConfigObjectException : LocalizedException
	{
		// Token: 0x060018AD RID: 6317 RVA: 0x0005C201 File Offset: 0x0005A401
		public AmbiguousConfigObjectException(string identity, Type classType) : base(Strings.ConfigObjectAmbiguous(identity, classType))
		{
			this.identity = identity;
			this.classType = classType;
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x0005C21E File Offset: 0x0005A41E
		public AmbiguousConfigObjectException(string identity, Type classType, Exception innerException) : base(Strings.ConfigObjectAmbiguous(identity, classType), innerException)
		{
			this.identity = identity;
			this.classType = classType;
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x0005C23C File Offset: 0x0005A43C
		protected AmbiguousConfigObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
			this.classType = (Type)info.GetValue("classType", typeof(Type));
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x0005C291 File Offset: 0x0005A491
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
			info.AddValue("classType", this.classType);
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x0005C2BD File Offset: 0x0005A4BD
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x0005C2C5 File Offset: 0x0005A4C5
		public Type ClassType
		{
			get
			{
				return this.classType;
			}
		}

		// Token: 0x0400097E RID: 2430
		private readonly string identity;

		// Token: 0x0400097F RID: 2431
		private readonly Type classType;
	}
}
