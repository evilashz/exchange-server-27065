using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Services.Providers;
using System.Linq;
using System.Reflection;
using System.Security.Principal;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000025 RID: 37
	internal class Metadata
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00003B68 File Offset: 0x00001D68
		public Metadata(IPrincipal user, Dictionary<string, IEntity>.ValueCollection entities, Dictionary<string, ResourceType> complexTypeResourceTypes)
		{
			this.user = user;
			this.ResourceTypes = new Dictionary<string, ResourceType>(entities.Count + complexTypeResourceTypes.Count);
			this.ResourceSets = new Dictionary<string, ResourceSet>(entities.Count);
			foreach (KeyValuePair<string, ResourceType> keyValuePair in complexTypeResourceTypes)
			{
				if (!this.ResourceTypes.ContainsKey(keyValuePair.Value.FullName))
				{
					keyValuePair.Value.SetReadOnly();
					this.ResourceTypes.Add(keyValuePair.Value.FullName, keyValuePair.Value);
				}
			}
			this.CreateEntitySchema(entities, complexTypeResourceTypes);
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003C30 File Offset: 0x00001E30
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00003C38 File Offset: 0x00001E38
		public Dictionary<string, ResourceType> ResourceTypes { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003C41 File Offset: 0x00001E41
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00003C49 File Offset: 0x00001E49
		public Dictionary<string, ResourceSet> ResourceSets { get; private set; }

		// Token: 0x060000C3 RID: 195 RVA: 0x00003C54 File Offset: 0x00001E54
		private void CreateEntitySchema(Dictionary<string, IEntity>.ValueCollection entities, Dictionary<string, ResourceType> complexTypeResourceTypes)
		{
			foreach (IEntity entity in entities)
			{
				string text = string.Format("{0}\\{1}", entity.TaskInvocationInfo.SnapinName, entity.TaskInvocationInfo.CmdletName);
				if (this.user.IsInRole(text))
				{
					bool flag = true;
					if (entity.TaskInvocationInfo.Parameters != null)
					{
						foreach (string param in entity.TaskInvocationInfo.Parameters.Keys)
						{
							if (!this.IsParamAllowedByRbac(text, param))
							{
								flag = false;
								break;
							}
						}
					}
					if (flag)
					{
						ResourceType resourceType = this.GetResourceType(entity, complexTypeResourceTypes);
						ResourceSet resourceSet = this.GetResourceSet(entity, resourceType);
						this.ResourceSets.Add(resourceSet.Name, resourceSet);
					}
				}
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003D64 File Offset: 0x00001F64
		private ResourceSet GetResourceSet(IEntity entity, ResourceType resourceType)
		{
			if (!Metadata.GlobalResourceSets.ContainsKey(entity.Name))
			{
				ResourceSet value = this.CreateResourceSet(entity, resourceType);
				Metadata.GlobalResourceSets.TryAdd(entity.Name, value);
			}
			return Metadata.GlobalResourceSets[entity.Name];
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003DB0 File Offset: 0x00001FB0
		private ResourceSet CreateResourceSet(IEntity entity, ResourceType resourceType)
		{
			ResourceSet resourceSet = new ResourceSet(entity.Name, resourceType);
			resourceSet.SetReadOnly();
			return resourceSet;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003DD4 File Offset: 0x00001FD4
		private ResourceType GetResourceType(IEntity entity, Dictionary<string, ResourceType> complexTypeResourceTypes)
		{
			ResourceType resourceType = new ResourceType(entity.ClrType, 0, null, "TenantReporting", entity.ClrType.Name, false);
			if (this.ResourceTypes.ContainsKey(resourceType.FullName))
			{
				resourceType = this.ResourceTypes[resourceType.FullName];
			}
			else
			{
				if (!Metadata.GlobalResourceTypes.ContainsKey(resourceType.FullName))
				{
					this.BuildResourceType(entity, resourceType, complexTypeResourceTypes);
					Metadata.GlobalResourceTypes.TryAdd(resourceType.FullName, resourceType);
				}
				resourceType = Metadata.GlobalResourceTypes[resourceType.FullName];
				this.ResourceTypes.Add(resourceType.FullName, resourceType);
			}
			return resourceType;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003E78 File Offset: 0x00002078
		private void BuildResourceType(IEntity entity, ResourceType resourceType, Dictionary<string, ResourceType> complexTypeResourceTypes)
		{
			if (ResourceType.GetPrimitiveResourceType(entity.ClrType) == null)
			{
				foreach (PropertyInfo propertyInfo in entity.ClrType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy))
				{
					if (entity.ReportPropertyCmdletParamsMap == null || !entity.ReportPropertyCmdletParamsMap.ContainsKey(propertyInfo.Name) || this.IsEntityPropertyVisibleForCurrentUser(entity, propertyInfo.Name))
					{
						Type type = propertyInfo.PropertyType;
						Type type2;
						if (ReportingSchema.IsNullableType(type, out type2))
						{
							type = type2;
						}
						ResourcePropertyKind resourcePropertyKind = 1;
						ResourceType resourceType2 = ResourceType.GetPrimitiveResourceType(type);
						if (resourceType2 == null)
						{
							if (type.IsEnum || type.IsValueType)
							{
								throw new NotSupportedException("struct and enum are not supported. For struct, try to change it to class. For enum, try to change it to integer or string.");
							}
							if (type.Equals(entity.ClrType))
							{
								resourceType2 = resourceType;
							}
							else
							{
								resourceType2 = complexTypeResourceTypes[type.FullName];
							}
							resourcePropertyKind = 4;
						}
						resourceType.AddProperty(new ResourceProperty(propertyInfo.Name, resourcePropertyKind | (entity.KeyMembers.Contains(propertyInfo.Name) ? 2 : 0), resourceType2));
					}
				}
			}
			resourceType.SetReadOnly();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003F84 File Offset: 0x00002184
		private bool IsEntityPropertyVisibleForCurrentUser(IEntity entity, string property)
		{
			foreach (string param in entity.ReportPropertyCmdletParamsMap[property])
			{
				if (!this.IsParamAllowedByRbac(entity.TaskInvocationInfo.CmdletName, param))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003FF4 File Offset: 0x000021F4
		private bool IsParamAllowedByRbac(string cmdlet, string param)
		{
			return this.user.IsInRole(string.Format("{0}?{1}", cmdlet, param));
		}

		// Token: 0x0400004E RID: 78
		private static readonly ConcurrentDictionary<string, ResourceType> GlobalResourceTypes = new ConcurrentDictionary<string, ResourceType>();

		// Token: 0x0400004F RID: 79
		private static readonly ConcurrentDictionary<string, ResourceSet> GlobalResourceSets = new ConcurrentDictionary<string, ResourceSet>();

		// Token: 0x04000050 RID: 80
		private IPrincipal user;
	}
}
