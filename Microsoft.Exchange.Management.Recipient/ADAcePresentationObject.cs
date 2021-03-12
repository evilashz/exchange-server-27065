using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000095 RID: 149
	[Serializable]
	public class ADAcePresentationObject : AcePresentationObject
	{
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00029D4B File Offset: 0x00027F4B
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ADAcePresentationObject.schema;
			}
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00029D54 File Offset: 0x00027F54
		public ADAcePresentationObject(ActiveDirectoryAccessRule ace, ADObjectId identity) : base(ace, identity)
		{
			this.AccessRights = new ActiveDirectoryRights[]
			{
				ace.ActiveDirectoryRights
			};
			base.ResetChangeTracking();
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00029D86 File Offset: 0x00027F86
		public ADAcePresentationObject()
		{
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x00029D8E File Offset: 0x00027F8E
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x00029DA0 File Offset: 0x00027FA0
		[Parameter(Mandatory = false, ParameterSetName = "Instance")]
		[Parameter(Mandatory = false, ParameterSetName = "AccessRights")]
		public ActiveDirectoryRights[] AccessRights
		{
			get
			{
				return (ActiveDirectoryRights[])this[ADAcePresentationObjectSchema.AccessRights];
			}
			set
			{
				this[ADAcePresentationObjectSchema.AccessRights] = value;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x00029DAE File Offset: 0x00027FAE
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x00029DC0 File Offset: 0x00027FC0
		[Parameter(Mandatory = false, ParameterSetName = "AccessRights")]
		[Parameter(Mandatory = false, ParameterSetName = "Instance")]
		public ExtendedRightIdParameter[] ExtendedRights
		{
			get
			{
				return (ExtendedRightIdParameter[])this[ADAcePresentationObjectSchema.ExtendedRights];
			}
			set
			{
				this[ADAcePresentationObjectSchema.ExtendedRights] = value;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x00029DCE File Offset: 0x00027FCE
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x00029DE0 File Offset: 0x00027FE0
		[Parameter(Mandatory = false, ParameterSetName = "AccessRights")]
		[Parameter(Mandatory = false, ParameterSetName = "Instance")]
		public ADSchemaObjectIdParameter[] ChildObjectTypes
		{
			get
			{
				return (ADSchemaObjectIdParameter[])this[ADAcePresentationObjectSchema.ChildObjectTypes];
			}
			set
			{
				this[ADAcePresentationObjectSchema.ChildObjectTypes] = value;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x00029DEE File Offset: 0x00027FEE
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x00029E00 File Offset: 0x00028000
		[Parameter(Mandatory = false, ParameterSetName = "Instance")]
		[Parameter(Mandatory = false, ParameterSetName = "AccessRights")]
		public ADSchemaObjectIdParameter InheritedObjectType
		{
			get
			{
				return (ADSchemaObjectIdParameter)this[ADAcePresentationObjectSchema.InheritedObjectType];
			}
			set
			{
				this[ADAcePresentationObjectSchema.InheritedObjectType] = value;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00029E0E File Offset: 0x0002800E
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x00029E20 File Offset: 0x00028020
		[Parameter(Mandatory = false, ParameterSetName = "AccessRights")]
		[Parameter(Mandatory = false, ParameterSetName = "Instance")]
		public ADSchemaObjectIdParameter[] Properties
		{
			get
			{
				return (ADSchemaObjectIdParameter[])this[ADAcePresentationObjectSchema.Properties];
			}
			set
			{
				this[ADAcePresentationObjectSchema.Properties] = value;
			}
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00029E30 File Offset: 0x00028030
		protected override void PopulateCalculatedProperties()
		{
			base.PopulateCalculatedProperties();
			TaskLogger.Trace("Resolving InheritedObjectType", new object[0]);
			IConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 175, "PopulateCalculatedProperties", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\permission\\ADAcePresentationObject.cs");
			if (base.RealAce.InheritedObjectType != Guid.Empty)
			{
				ADSchemaObjectIdParameter adschemaObjectIdParameter = ADSchemaObjectIdParameter.Parse(base.RealAce.InheritedObjectType.ToString());
				IEnumerable<ADSchemaClassObject> objects = adschemaObjectIdParameter.GetObjects<ADSchemaClassObject>(null, session);
				using (IEnumerator<ADSchemaClassObject> enumerator = objects.GetEnumerator())
				{
					string text = null;
					if (enumerator.MoveNext())
					{
						text = enumerator.Current.Name;
					}
					if (text == null || enumerator.MoveNext())
					{
						this.InheritedObjectType = ADSchemaObjectIdParameter.Parse(base.RealAce.InheritedObjectType.ToString());
						TaskLogger.Trace("Could not resolve the following InheritedObjectType: {0}", new object[]
						{
							base.RealAce.InheritedObjectType.ToString()
						});
					}
					else
					{
						this.InheritedObjectType = ADSchemaObjectIdParameter.Parse(text);
					}
				}
			}
			if (base.RealAce.ObjectType != Guid.Empty)
			{
				ActiveDirectoryRights activeDirectoryRights = base.RealAce.ActiveDirectoryRights;
				string text2 = base.RealAce.ObjectType.ToString();
				bool flag = false;
				if ((activeDirectoryRights & ActiveDirectoryRights.ExtendedRight) == ActiveDirectoryRights.ExtendedRight)
				{
					TaskLogger.Trace("Resolving ExtendedRight", new object[0]);
					ExtendedRightIdParameter[] extendedRights = null;
					ExtendedRightIdParameter extendedRightIdParameter = ExtendedRightIdParameter.Parse(text2);
					IEnumerable<ExtendedRight> objects2 = extendedRightIdParameter.GetObjects<ExtendedRight>(null, session);
					using (IEnumerator<ExtendedRight> enumerator2 = objects2.GetEnumerator())
					{
						string text3 = null;
						if (enumerator2.MoveNext())
						{
							text3 = enumerator2.Current.Name;
						}
						if (text3 != null && !enumerator2.MoveNext())
						{
							extendedRights = new ExtendedRightIdParameter[]
							{
								ExtendedRightIdParameter.Parse(text3)
							};
							flag = true;
						}
					}
					this.ExtendedRights = extendedRights;
				}
				if ((!flag && (activeDirectoryRights & ActiveDirectoryRights.CreateChild) == ActiveDirectoryRights.CreateChild) || (activeDirectoryRights & ActiveDirectoryRights.DeleteChild) == ActiveDirectoryRights.DeleteChild || (activeDirectoryRights & ActiveDirectoryRights.ReadProperty) == ActiveDirectoryRights.ReadProperty || (activeDirectoryRights & ActiveDirectoryRights.WriteProperty) == ActiveDirectoryRights.WriteProperty)
				{
					TaskLogger.Trace("Resolving Child Object Type", new object[0]);
					ADSchemaObjectIdParameter[] childObjectTypes = null;
					ADSchemaObjectIdParameter adschemaObjectIdParameter2 = ADSchemaObjectIdParameter.Parse(text2);
					IEnumerable<ADSchemaClassObject> objects3 = adschemaObjectIdParameter2.GetObjects<ADSchemaClassObject>(null, session);
					using (IEnumerator<ADSchemaClassObject> enumerator3 = objects3.GetEnumerator())
					{
						string text4 = null;
						if (enumerator3.MoveNext())
						{
							text4 = enumerator3.Current.Name;
						}
						if (text4 != null && !enumerator3.MoveNext())
						{
							childObjectTypes = new ADSchemaObjectIdParameter[]
							{
								ADSchemaObjectIdParameter.Parse(text4)
							};
							flag = true;
						}
					}
					this.ChildObjectTypes = childObjectTypes;
				}
				if ((!flag && (activeDirectoryRights & ActiveDirectoryRights.ReadProperty) == ActiveDirectoryRights.ReadProperty) || (activeDirectoryRights & ActiveDirectoryRights.WriteProperty) == ActiveDirectoryRights.WriteProperty || (activeDirectoryRights & ActiveDirectoryRights.Self) == ActiveDirectoryRights.Self)
				{
					TaskLogger.Trace("Resolving Property", new object[0]);
					ADSchemaObjectIdParameter[] properties = null;
					ADSchemaObjectIdParameter adschemaObjectIdParameter3 = ADSchemaObjectIdParameter.Parse(text2);
					IEnumerable<ADSchemaAttributeObject> objects4 = adschemaObjectIdParameter3.GetObjects<ADSchemaAttributeObject>(null, session);
					using (IEnumerator<ADSchemaAttributeObject> enumerator4 = objects4.GetEnumerator())
					{
						string text5 = null;
						if (enumerator4.MoveNext())
						{
							text5 = enumerator4.Current.Name;
						}
						if (text5 == null || enumerator4.MoveNext())
						{
							ExtendedRightIdParameter extendedRightIdParameter2 = ExtendedRightIdParameter.Parse(text2);
							IEnumerable<ExtendedRight> objects5 = extendedRightIdParameter2.GetObjects<ExtendedRight>(null, session);
							using (IEnumerator<ExtendedRight> enumerator5 = objects5.GetEnumerator())
							{
								string text6 = null;
								if (enumerator5.MoveNext())
								{
									text6 = enumerator5.Current.Name;
								}
								TaskLogger.Trace("Could not resolve the following property: {0}", new object[]
								{
									text2
								});
								if (text6 != null && !enumerator5.MoveNext())
								{
									properties = new ADSchemaObjectIdParameter[]
									{
										ADSchemaObjectIdParameter.Parse(text6)
									};
								}
								goto IL_3A4;
							}
						}
						properties = new ADSchemaObjectIdParameter[]
						{
							ADSchemaObjectIdParameter.Parse(text5)
						};
						IL_3A4:;
					}
					this.Properties = properties;
				}
			}
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0002A238 File Offset: 0x00028438
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (base.RealAce != null && base.RealAce.ObjectType != Guid.Empty && this.ExtendedRights == null && this.ChildObjectTypes == null && this.Properties == null)
			{
				errors.Add(new ObjectValidationError(Strings.CannotResolveObjectTypeDefined(base.RealAce.ObjectType.ToString()), base.Identity, string.Empty));
			}
		}

		// Token: 0x04000205 RID: 517
		private static ADAcePresentationObjectSchema schema = ObjectSchema.GetInstance<ADAcePresentationObjectSchema>();
	}
}
