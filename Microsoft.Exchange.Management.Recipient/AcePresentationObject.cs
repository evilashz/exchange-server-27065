using System;
using System.DirectoryServices;
using System.Management.Automation;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000093 RID: 147
	[Serializable]
	public abstract class AcePresentationObject : ConfigurableObject
	{
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x00029A98 File Offset: 0x00027C98
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return AcePresentationObject.schema;
			}
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00029A9F File Offset: 0x00027C9F
		public AcePresentationObject(ActiveDirectoryAccessRule ace, ObjectId identity) : this()
		{
			this.realAce = ace;
			this.Identity = identity;
			this.InheritanceType = ace.InheritanceType;
			this.IsInherited = ace.IsInherited;
			this.PopulateCalculatedProperties();
			base.ResetChangeTracking();
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00029AD9 File Offset: 0x00027CD9
		public AcePresentationObject() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00029AE6 File Offset: 0x00027CE6
		// (set) Token: 0x060009E6 RID: 2534 RVA: 0x00029AFD File Offset: 0x00027CFD
		[Parameter(Mandatory = false, ParameterSetName = "Instance")]
		[Parameter(Mandatory = false, ParameterSetName = "AccessRights")]
		public SwitchParameter Deny
		{
			get
			{
				return (bool)this[AcePresentationObjectSchema.Deny];
			}
			set
			{
				this[AcePresentationObjectSchema.Deny] = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x00029B15 File Offset: 0x00027D15
		// (set) Token: 0x060009E8 RID: 2536 RVA: 0x00029B27 File Offset: 0x00027D27
		[Parameter(Mandatory = false, ParameterSetName = "Instance")]
		[Parameter(Mandatory = false, ParameterSetName = "AccessRights")]
		public ActiveDirectorySecurityInheritance InheritanceType
		{
			get
			{
				return (ActiveDirectorySecurityInheritance)this[AcePresentationObjectSchema.InheritanceType];
			}
			set
			{
				this[AcePresentationObjectSchema.InheritanceType] = value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00029B3C File Offset: 0x00027D3C
		// (set) Token: 0x060009EA RID: 2538 RVA: 0x00029B7B File Offset: 0x00027D7B
		[Parameter(Mandatory = false, ParameterSetName = "Instance")]
		[Parameter(Mandatory = true, ParameterSetName = "AccessRights")]
		public SecurityPrincipalIdParameter User
		{
			get
			{
				SecurityPrincipalIdParameter securityPrincipalIdParameter = (SecurityPrincipalIdParameter)this[AcePresentationObjectSchema.User];
				if (securityPrincipalIdParameter != null && SuppressingPiiContext.NeedPiiSuppression)
				{
					securityPrincipalIdParameter = new SecurityPrincipalIdParameter(SuppressingPiiData.Redact(securityPrincipalIdParameter.SecurityIdentifier.Value));
				}
				return securityPrincipalIdParameter;
			}
			set
			{
				this[AcePresentationObjectSchema.User] = value;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x00029B8C File Offset: 0x00027D8C
		// (set) Token: 0x060009EC RID: 2540 RVA: 0x00029BC6 File Offset: 0x00027DC6
		public new ObjectId Identity
		{
			get
			{
				ObjectId objectId = base.Identity;
				if (objectId is ADObjectId && SuppressingPiiContext.NeedPiiSuppression)
				{
					objectId = (ObjectId)SuppressingPiiProperty.TryRedact(ADObjectSchema.Id, (ADObjectId)objectId);
				}
				return objectId;
			}
			set
			{
				this[SimpleProviderObjectSchema.Identity] = value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x00029BD4 File Offset: 0x00027DD4
		// (set) Token: 0x060009EE RID: 2542 RVA: 0x00029BE6 File Offset: 0x00027DE6
		public bool IsInherited
		{
			get
			{
				return (bool)this[AcePresentationObjectSchema.IsInherited];
			}
			internal set
			{
				this[AcePresentationObjectSchema.IsInherited] = value;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00029BF9 File Offset: 0x00027DF9
		protected ActiveDirectoryAccessRule RealAce
		{
			get
			{
				return this.realAce;
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00029C04 File Offset: 0x00027E04
		protected virtual void PopulateCalculatedProperties()
		{
			TaskLogger.Trace("Resolving sid", new object[0]);
			SecurityIdentifier sid = (SecurityIdentifier)this.RealAce.IdentityReference;
			this.User = new SecurityPrincipalIdParameter(sid);
			this.Deny = (this.realAce.AccessControlType == AccessControlType.Deny);
		}

		// Token: 0x040001FE RID: 510
		private static AcePresentationObjectSchema schema = ObjectSchema.GetInstance<AcePresentationObjectSchema>();

		// Token: 0x040001FF RID: 511
		[NonSerialized]
		private ActiveDirectoryAccessRule realAce;
	}
}
