using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Directory.Provider;

namespace Microsoft.Exchange.UnifiedGroups
{
	// Token: 0x02000058 RID: 88
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SharePointNotification : IDisposeTrackable, IDisposable
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x00010E2C File Offset: 0x0000F02C
		public SharePointNotification(SharePointNotification.NotificationType notificationType, string externalDirectoryObjectId, OrganizationId organizationId, ICredentials actAsUserCredentials, Guid activityId)
		{
			this.notificationType = notificationType;
			this.externalDirectoryObjectId = externalDirectoryObjectId;
			this.organizationId = organizationId;
			this.actAsUserCredentials = actAsUserCredentials;
			this.activityId = activityId;
			this.directoryObjectData = this.CreateDirectoryObjectData();
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00010E7C File Offset: 0x0000F07C
		private MemoryStream HelperStream
		{
			get
			{
				if (this.helperStream == null)
				{
					this.helperStream = new MemoryStream(4096);
				}
				return this.helperStream;
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00010E9C File Offset: 0x0000F09C
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SharePointNotification>(this);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00010EA4 File Offset: 0x0000F0A4
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00010EB9 File Offset: 0x0000F0B9
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			if (this.helperStream != null)
			{
				this.helperStream.Dispose();
				this.helperStream = null;
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00010EF0 File Offset: 0x0000F0F0
		public void Execute()
		{
			ClientRuntimeContext tenantClientContext = this.GetTenantClientContext();
			if (tenantClientContext == null)
			{
				SharePointNotification.Tracer.TraceDebug((long)this.GetHashCode(), "No ClientContext, skipping call to SharePoint");
				return;
			}
			using (tenantClientContext)
			{
				SharePointDirectoryProvider sharePointDirectoryProvider = new SharePointDirectoryProvider(tenantClientContext);
				if (this.notificationType == SharePointNotification.NotificationType.Create)
				{
					this.directoryObjectData.Version = -1L;
					sharePointDirectoryProvider.CreateDirectoryObject(this.directoryObjectData);
				}
				else if (this.notificationType == SharePointNotification.NotificationType.Update)
				{
					sharePointDirectoryProvider.NotifyDataChanges(this.directoryObjectData);
				}
				else if (this.notificationType == SharePointNotification.NotificationType.Delete)
				{
					sharePointDirectoryProvider.DeleteDirectoryObject(this.directoryObjectData);
				}
				tenantClientContext.RequestTimeout = 180000;
				tenantClientContext.ExecuteQuery();
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00010FA4 File Offset: 0x0000F1A4
		public void AddOwners(params string[] objectIds)
		{
			this.AddRelations("Owners", objectIds);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00010FB2 File Offset: 0x0000F1B2
		public void RemoveOwners(params string[] objectIds)
		{
			this.RemoveRelations("Owners", objectIds);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00010FC0 File Offset: 0x0000F1C0
		public void AddMembers(params string[] objectIds)
		{
			this.AddRelations("Members", objectIds);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00010FCE File Offset: 0x0000F1CE
		public void RemoveMembers(params string[] objectIds)
		{
			this.RemoveRelations("Members", objectIds);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00010FDC File Offset: 0x0000F1DC
		public void SetAllowAccessTo(bool isPublic)
		{
			if (isPublic)
			{
				this.AddRelations("AllowAccessTo", new string[]
				{
					SharePointNotification.EveryoneGroupId.ToString()
				});
				return;
			}
			this.AddRelations("AllowAccessTo", null);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00011024 File Offset: 0x0000F224
		public void SetPropertyValue(string name, object value, bool delayLoad = false)
		{
			PropertyData propertyData = new PropertyData();
			propertyData.Name = name;
			propertyData.DelayLoad = delayLoad;
			propertyData.Value = this.SerializeObjectBinary(value);
			this.directoryObjectData.Properties[name] = propertyData;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00011064 File Offset: 0x0000F264
		public void SetResourceValue(string name, object value, bool delayLoad = false)
		{
			ResourceData resourceData = new ResourceData();
			resourceData.Name = name;
			resourceData.DelayLoad = delayLoad;
			resourceData.Value = this.SerializeObjectBinary(value);
			this.directoryObjectData.Resources[name] = resourceData;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000110A4 File Offset: 0x0000F2A4
		private static Guid GetTenantContextId(OrganizationId organizationId)
		{
			if (organizationId == OrganizationId.ForestWideOrgId)
			{
				return Guid.Empty;
			}
			return new Guid(organizationId.ToExternalDirectoryOrganizationId());
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000110C4 File Offset: 0x0000F2C4
		private DirectoryObjectData CreateDirectoryObjectData()
		{
			DirectoryObjectData directoryObjectData = new DirectoryObjectData
			{
				TenantContextId = SharePointNotification.GetTenantContextId(this.organizationId),
				Id = new Guid(this.externalDirectoryObjectId),
				DirectoryObjectType = 2
			};
			directoryObjectData.RelationSets = new Dictionary<string, RelationSetData>();
			directoryObjectData.Properties = new Dictionary<string, PropertyData>();
			directoryObjectData.Resources = new Dictionary<string, ResourceData>();
			directoryObjectData.States = new Dictionary<string, StateData>();
			return directoryObjectData;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00011130 File Offset: 0x0000F330
		private RelationSetData CreateRelationSetData(string name)
		{
			return new RelationSetData
			{
				AddedRelations = new Dictionary<string, RelationData>(),
				Relations = new Dictionary<string, RelationData>(),
				RemovedRelations = new Dictionary<string, RelationData>(),
				Name = name,
				DelayLoad = true
			};
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000111D4 File Offset: 0x0000F3D4
		private ClientContext GetTenantClientContext()
		{
			Uri rootSiteUrl = SharePointUrl.GetRootSiteUrl(this.organizationId);
			if (rootSiteUrl == null)
			{
				return null;
			}
			ClientContext clientContext = new ClientContext(rootSiteUrl);
			clientContext.ExecutingWebRequest += delegate(object sender, WebRequestEventArgs request)
			{
				request.WebRequestExecutor.RequestHeaders.Add(HttpRequestHeader.Authorization, "Bearer");
				request.WebRequestExecutor.RequestHeaders.Add("SPResponseGuid", this.activityId.ToString());
				request.WebRequestExecutor.WebRequest.PreAuthenticate = true;
			};
			clientContext.Credentials = this.actAsUserCredentials;
			return clientContext;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00011220 File Offset: 0x0000F420
		private void AddRelations(string name, params string[] objectIds)
		{
			RelationSetData relationSetData = this.CreateRelationSetData(name);
			if (objectIds != null)
			{
				foreach (string text in objectIds)
				{
					Relation value = new Relation(SharePointNotification.RelationTypeIds[name], new Guid(text));
					RelationData relationData = new RelationData();
					relationData.Value = this.SerializeObjectXML<Relation>(value);
					relationSetData.AddedRelations[text] = relationData;
				}
			}
			this.directoryObjectData.RelationSets[name] = relationSetData;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000112A0 File Offset: 0x0000F4A0
		private void RemoveRelations(string name, params string[] objectIds)
		{
			RelationSetData relationSetData = this.CreateRelationSetData(name);
			if (objectIds != null)
			{
				foreach (string text in objectIds)
				{
					Relation value = new Relation(SharePointNotification.RelationTypeIds[name], new Guid(text));
					RelationData relationData = new RelationData();
					relationData.Value = this.SerializeObjectXML<Relation>(value);
					relationSetData.RemovedRelations[text] = relationData;
				}
			}
			this.directoryObjectData.RelationSets[name] = relationSetData;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00011320 File Offset: 0x0000F520
		private byte[] SerializeObjectXML<T>(T value) where T : class
		{
			DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));
			dataContractSerializer.WriteObject(this.HelperStream, value);
			byte[] array = new byte[this.HelperStream.Length];
			Array.Copy(this.HelperStream.GetBuffer(), array, array.Length);
			this.ResetHelperStream();
			return array;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0001137C File Offset: 0x0000F57C
		private byte[] SerializeObjectBinary(object value)
		{
			if (value == null)
			{
				return null;
			}
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
			binaryFormatter.Serialize(this.HelperStream, value);
			byte[] array = new byte[this.HelperStream.Length];
			Array.Copy(this.HelperStream.GetBuffer(), array, array.Length);
			this.ResetHelperStream();
			return array;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000113CF File Offset: 0x0000F5CF
		private void ResetHelperStream()
		{
			this.HelperStream.Seek(0L, SeekOrigin.Begin);
			this.HelperStream.SetLength(0L);
		}

		// Token: 0x04000189 RID: 393
		private const string OwnersRelationName = "Owners";

		// Token: 0x0400018A RID: 394
		private const string MembersRelationName = "Members";

		// Token: 0x0400018B RID: 395
		private const string AllowAccessToRelationName = "AllowAccessTo";

		// Token: 0x0400018C RID: 396
		private const string SPResponseGuidHeader = "SPResponseGuid";

		// Token: 0x0400018D RID: 397
		public static readonly Guid EveryoneGroupId = new Guid("C41554C4-1734-4462-9544-5E5542F2EB1C");

		// Token: 0x0400018E RID: 398
		internal static readonly Trace Tracer = ExTraceGlobals.ModernGroupsTracer;

		// Token: 0x0400018F RID: 399
		private static readonly Dictionary<string, Guid> RelationTypeIds = new Dictionary<string, Guid>
		{
			{
				"Owners",
				new Guid("cf58be5f-dd0f-4321-982e-72deaccca8a4")
			},
			{
				"Members",
				new Guid("3d420ade-03d2-493c-831b-adebde7b9702")
			},
			{
				"AllowAccessTo",
				new Guid("1E87A52F-0324-46F8-9170-F66EEFDEFC7D")
			}
		};

		// Token: 0x04000190 RID: 400
		private readonly string externalDirectoryObjectId;

		// Token: 0x04000191 RID: 401
		private readonly Guid activityId;

		// Token: 0x04000192 RID: 402
		private DirectoryObjectData directoryObjectData;

		// Token: 0x04000193 RID: 403
		private SharePointNotification.NotificationType notificationType;

		// Token: 0x04000194 RID: 404
		private OrganizationId organizationId;

		// Token: 0x04000195 RID: 405
		private ICredentials actAsUserCredentials;

		// Token: 0x04000196 RID: 406
		private MemoryStream helperStream;

		// Token: 0x04000197 RID: 407
		private DisposeTracker disposeTracker;

		// Token: 0x02000059 RID: 89
		public enum NotificationType
		{
			// Token: 0x04000199 RID: 409
			Create,
			// Token: 0x0400019A RID: 410
			Update,
			// Token: 0x0400019B RID: 411
			Delete
		}
	}
}
