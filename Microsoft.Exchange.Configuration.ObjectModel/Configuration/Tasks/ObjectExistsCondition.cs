using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.ObjectModel;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200007D RID: 125
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class ObjectExistsCondition : Condition
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x0001176C File Offset: 0x0000F96C
		public ObjectExistsCondition(string identity, Type type)
		{
			this.ObjectIdentity = identity;
			this.ObjectType = type;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00011784 File Offset: 0x0000F984
		public override bool Verify()
		{
			TaskLogger.LogEnter();
			if ("" == this.ObjectIdentity)
			{
				throw new ConditionInitializationException("ObjectIdentity", this);
			}
			if (this.ObjectIdentity == null)
			{
				throw new ConditionInitializationException("ObjectIdentity", this);
			}
			if (null == this.ObjectType)
			{
				throw new ConditionInitializationException("ObjectType", this);
			}
			if (!this.ObjectType.IsSubclassOf(typeof(ConfigObject)) || this.ObjectType.IsAbstract)
			{
				throw new ConditionInitializationException("ObjectType", this, new LocalizedException(Strings.ExceptionInvalidConfigObjectType(this.ObjectType)));
			}
			ConfigObject configObject = ConfigObjectReader.FindById(this.ObjectType, this.ObjectIdentity);
			bool result = configObject != null;
			TaskLogger.LogExit();
			return result;
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00011844 File Offset: 0x0000FA44
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x0001184C File Offset: 0x0000FA4C
		public string ObjectIdentity
		{
			get
			{
				return this.objectIdentity;
			}
			set
			{
				this.objectIdentity = value;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00011855 File Offset: 0x0000FA55
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x0001185D File Offset: 0x0000FA5D
		public Type ObjectType
		{
			get
			{
				return this.objectType;
			}
			set
			{
				this.objectType = value;
			}
		}

		// Token: 0x0400011A RID: 282
		private string objectIdentity;

		// Token: 0x0400011B RID: 283
		private Type objectType;
	}
}
