using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E55 RID: 3669
	internal class FolderSchema : EntitySchema
	{
		// Token: 0x17001579 RID: 5497
		// (get) Token: 0x06005E70 RID: 24176 RVA: 0x001265B4 File Offset: 0x001247B4
		public new static FolderSchema SchemaInstance
		{
			get
			{
				return FolderSchema.FolderSchemaInstance.Member;
			}
		}

		// Token: 0x1700157A RID: 5498
		// (get) Token: 0x06005E71 RID: 24177 RVA: 0x001265C0 File Offset: 0x001247C0
		public override EdmEntityType EdmEntityType
		{
			get
			{
				return Folder.EdmEntityType;
			}
		}

		// Token: 0x1700157B RID: 5499
		// (get) Token: 0x06005E72 RID: 24178 RVA: 0x001265C7 File Offset: 0x001247C7
		public override ReadOnlyCollection<PropertyDefinition> DeclaredProperties
		{
			get
			{
				return FolderSchema.DeclaredFolderProperties;
			}
		}

		// Token: 0x1700157C RID: 5500
		// (get) Token: 0x06005E73 RID: 24179 RVA: 0x001265CE File Offset: 0x001247CE
		public override ReadOnlyCollection<PropertyDefinition> AllProperties
		{
			get
			{
				return FolderSchema.AllFolderProperties;
			}
		}

		// Token: 0x1700157D RID: 5501
		// (get) Token: 0x06005E74 RID: 24180 RVA: 0x001265D5 File Offset: 0x001247D5
		public override ReadOnlyCollection<PropertyDefinition> DefaultProperties
		{
			get
			{
				return FolderSchema.DefaultFolderProperties;
			}
		}

		// Token: 0x1700157E RID: 5502
		// (get) Token: 0x06005E75 RID: 24181 RVA: 0x001265DC File Offset: 0x001247DC
		public override ReadOnlyCollection<PropertyDefinition> MandatoryCreationProperties
		{
			get
			{
				return FolderSchema.MandatoryFolderCreationProperties;
			}
		}

		// Token: 0x06005E76 RID: 24182 RVA: 0x001265E4 File Offset: 0x001247E4
		public override void RegisterEdmModel(EdmModel model)
		{
			base.RegisterEdmModel(model);
			CustomActions.RegisterAction(model, Folder.EdmEntityType, Folder.EdmEntityType, "Copy", new Dictionary<string, IEdmTypeReference>
			{
				{
					"DestinationId",
					EdmCoreModel.Instance.GetString(true)
				}
			});
			CustomActions.RegisterAction(model, Folder.EdmEntityType, Folder.EdmEntityType, "Move", new Dictionary<string, IEdmTypeReference>
			{
				{
					"DestinationId",
					EdmCoreModel.Instance.GetString(true)
				}
			});
		}

		// Token: 0x06005E78 RID: 24184 RVA: 0x001266D0 File Offset: 0x001248D0
		// Note: this type is marked as 'beforefieldinit'.
		static FolderSchema()
		{
			PropertyDefinition propertyDefinition = new PropertyDefinition("ParentFolderId", typeof(string));
			propertyDefinition.EdmType = EdmCoreModel.Instance.GetString(true);
			PropertyDefinition propertyDefinition2 = propertyDefinition;
			SimpleEwsPropertyProvider simpleEwsPropertyProvider = new SimpleEwsPropertyProvider(BaseFolderSchema.ParentFolderId);
			simpleEwsPropertyProvider.Getter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				e[ep] = EwsIdConverter.EwsIdToODataId((s[sp] as FolderId).Id);
			};
			propertyDefinition2.EwsPropertyProvider = simpleEwsPropertyProvider;
			FolderSchema.ParentFolderId = propertyDefinition;
			PropertyDefinition propertyDefinition3 = new PropertyDefinition("UnreadItemCount", typeof(int));
			propertyDefinition3.EdmType = EdmCoreModel.Instance.GetInt32(true);
			propertyDefinition3.Flags = PropertyDefinitionFlags.CanFilter;
			PropertyDefinition propertyDefinition4 = propertyDefinition3;
			SimpleEwsPropertyProvider simpleEwsPropertyProvider2 = new SimpleEwsPropertyProvider(FolderSchema.UnreadCount);
			simpleEwsPropertyProvider2.Getter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				e[ep] = (s[sp] ?? 0);
			};
			propertyDefinition4.EwsPropertyProvider = simpleEwsPropertyProvider2;
			FolderSchema.UnreadItemCount = propertyDefinition3;
			PropertyDefinition propertyDefinition5 = new PropertyDefinition("TotalCount", typeof(int));
			propertyDefinition5.EdmType = EdmCoreModel.Instance.GetInt32(true);
			propertyDefinition5.Flags = PropertyDefinitionFlags.CanFilter;
			PropertyDefinition propertyDefinition6 = propertyDefinition5;
			SimpleEwsPropertyProvider simpleEwsPropertyProvider3 = new SimpleEwsPropertyProvider(BaseFolderSchema.TotalCount);
			simpleEwsPropertyProvider3.Getter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				e[ep] = (s[sp] ?? 0);
			};
			propertyDefinition6.EwsPropertyProvider = simpleEwsPropertyProvider3;
			FolderSchema.TotalCount = propertyDefinition5;
			PropertyDefinition propertyDefinition7 = new PropertyDefinition("ChildFolderCount", typeof(int));
			propertyDefinition7.EdmType = EdmCoreModel.Instance.GetInt32(true);
			propertyDefinition7.Flags = PropertyDefinitionFlags.CanFilter;
			PropertyDefinition propertyDefinition8 = propertyDefinition7;
			SimpleEwsPropertyProvider simpleEwsPropertyProvider4 = new SimpleEwsPropertyProvider(BaseFolderSchema.ChildFolderCount);
			simpleEwsPropertyProvider4.Getter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				e[ep] = (s[sp] ?? 0);
			};
			propertyDefinition8.EwsPropertyProvider = simpleEwsPropertyProvider4;
			FolderSchema.ChildFolderCount = propertyDefinition7;
			FolderSchema.ChildFolders = new PropertyDefinition("ChildFolders", typeof(IEnumerable<Folder>))
			{
				Flags = PropertyDefinitionFlags.Navigation,
				NavigationTargetEntity = Folder.EdmEntityType
			};
			FolderSchema.Messages = new PropertyDefinition("Messages", typeof(IEnumerable<Message>))
			{
				Flags = PropertyDefinitionFlags.Navigation,
				NavigationTargetEntity = Message.EdmEntityType
			};
			FolderSchema.DeclaredFolderProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>
			{
				FolderSchema.ParentFolderId,
				FolderSchema.DisplayName,
				FolderSchema.ClassName,
				FolderSchema.TotalCount,
				FolderSchema.ChildFolderCount,
				FolderSchema.UnreadItemCount,
				FolderSchema.ChildFolders,
				FolderSchema.Messages
			});
			FolderSchema.AllFolderProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>(EntitySchema.AllEntityProperties.Union(FolderSchema.DeclaredFolderProperties)));
			FolderSchema.DefaultFolderProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>(EntitySchema.DefaultEntityProperties)
			{
				FolderSchema.ParentFolderId,
				FolderSchema.DisplayName,
				FolderSchema.ClassName,
				FolderSchema.TotalCount,
				FolderSchema.ChildFolderCount,
				FolderSchema.UnreadItemCount
			});
			FolderSchema.MandatoryFolderCreationProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>
			{
				FolderSchema.DisplayName
			});
			FolderSchema.FolderSchemaInstance = new LazyMember<FolderSchema>(() => new FolderSchema());
		}

		// Token: 0x04003318 RID: 13080
		public static readonly PropertyDefinition DisplayName = new PropertyDefinition("DisplayName", typeof(string))
		{
			EdmType = EdmCoreModel.Instance.GetString(true),
			Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
			EwsPropertyProvider = new SimpleEwsPropertyProvider(BaseFolderSchema.DisplayName)
			{
				SetPropertyUpdateCreator = EwsPropertyProvider.SetFolderPropertyUpdateDelegate,
				DeletePropertyUpdateCreator = EwsPropertyProvider.DeleteFolderPropertyUpdateDelegate
			}
		};

		// Token: 0x04003319 RID: 13081
		public static readonly PropertyDefinition ClassName = new PropertyDefinition("ClassName", typeof(string))
		{
			EdmType = EdmCoreModel.Instance.GetString(true),
			Flags = PropertyDefinitionFlags.CanFilter,
			EwsPropertyProvider = new SimpleEwsPropertyProvider(BaseFolderSchema.FolderClass)
			{
				SetPropertyUpdateCreator = EwsPropertyProvider.SetFolderPropertyUpdateDelegate,
				DeletePropertyUpdateCreator = EwsPropertyProvider.DeleteFolderPropertyUpdateDelegate
			}
		};

		// Token: 0x0400331A RID: 13082
		public static readonly PropertyDefinition ParentFolderId;

		// Token: 0x0400331B RID: 13083
		public static readonly PropertyDefinition UnreadItemCount;

		// Token: 0x0400331C RID: 13084
		public static readonly PropertyDefinition TotalCount;

		// Token: 0x0400331D RID: 13085
		public static readonly PropertyDefinition ChildFolderCount;

		// Token: 0x0400331E RID: 13086
		public static readonly PropertyDefinition ChildFolders;

		// Token: 0x0400331F RID: 13087
		public static readonly PropertyDefinition Messages;

		// Token: 0x04003320 RID: 13088
		public static readonly ReadOnlyCollection<PropertyDefinition> DeclaredFolderProperties;

		// Token: 0x04003321 RID: 13089
		public static readonly ReadOnlyCollection<PropertyDefinition> AllFolderProperties;

		// Token: 0x04003322 RID: 13090
		public static readonly ReadOnlyCollection<PropertyDefinition> DefaultFolderProperties;

		// Token: 0x04003323 RID: 13091
		public static readonly ReadOnlyCollection<PropertyDefinition> MandatoryFolderCreationProperties;

		// Token: 0x04003324 RID: 13092
		private static readonly LazyMember<FolderSchema> FolderSchemaInstance;
	}
}
