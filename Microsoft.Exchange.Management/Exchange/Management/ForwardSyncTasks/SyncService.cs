using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000369 RID: 873
	public abstract class SyncService : DisposeTrackableBase, IClientMessageInspector, IEndpointBehavior
	{
		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06001E7A RID: 7802 RVA: 0x000844DB File Offset: 0x000826DB
		public IDirectorySync SyncProxy
		{
			get
			{
				return this.SyncClient;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06001E7B RID: 7803 RVA: 0x000844E3 File Offset: 0x000826E3
		// (set) Token: 0x06001E7C RID: 7804 RVA: 0x000844EB File Offset: 0x000826EB
		public DirectorySyncClient SyncClient { get; private set; }

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06001E7D RID: 7805 RVA: 0x000844F4 File Offset: 0x000826F4
		// (set) Token: 0x06001E7E RID: 7806 RVA: 0x000844FC File Offset: 0x000826FC
		public string RawResponse { get; private set; }

		// Token: 0x06001E7F RID: 7807 RVA: 0x00084505 File Offset: 0x00082705
		protected SyncService()
		{
			this.InitializeSyncServiceProxy();
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x00084520 File Offset: 0x00082720
		public DirectoryObjectsAndLinks GetMsoRawObject(SyncObjectId syncObjectId, string serviceInstanceId, bool includeBackLinks, bool includeForwardLinks, int linksResultSize, out bool? allLinksCollected)
		{
			DirectoryObjectIdentity[] array = new DirectoryObjectIdentity[]
			{
				syncObjectId.ToMsoIdentity()
			};
			DirectoryObject[] array2 = new DirectoryObject[1];
			DirectoryLink[] array3 = new DirectoryLink[0];
			DirectoryObjectError[] array4 = new DirectoryObjectError[0];
			DirectoryObjectsAndLinks directoryObjectsAndLinks = new DirectoryObjectsAndLinks
			{
				NextPageToken = null,
				More = true
			};
			byte[] msoSyncCookie = this.GetMsoSyncCookie(serviceInstanceId);
			GetDirectoryObjectsOptions? getDirectoryObjectsOptions = new GetDirectoryObjectsOptions?(GetDirectoryObjectsOptions.None);
			if (includeBackLinks)
			{
				getDirectoryObjectsOptions |= GetDirectoryObjectsOptions.IncludeBackLinks;
			}
			if (includeForwardLinks)
			{
				getDirectoryObjectsOptions |= GetDirectoryObjectsOptions.IncludeForwardLinks;
			}
			if (includeForwardLinks || includeBackLinks)
			{
				allLinksCollected = new bool?(true);
			}
			else
			{
				allLinksCollected = null;
			}
			while (directoryObjectsAndLinks.More)
			{
				GetDirectoryObjectsRequest request = new GetDirectoryObjectsRequest((directoryObjectsAndLinks.NextPageToken == null) ? msoSyncCookie : null, (directoryObjectsAndLinks.NextPageToken == null) ? array : null, (directoryObjectsAndLinks.NextPageToken == null) ? getDirectoryObjectsOptions : null, directoryObjectsAndLinks.NextPageToken);
				GetDirectoryObjectsResponse directoryObjects = this.SyncProxy.GetDirectoryObjects(request);
				if (directoryObjects.GetDirectoryObjectsResult.Objects != null && directoryObjects.GetDirectoryObjectsResult.Objects.Length != 0 && array2[0] == null)
				{
					directoryObjects.GetDirectoryObjectsResult.Objects.CopyTo(array2, 0);
				}
				if (allLinksCollected == true && directoryObjects.GetDirectoryObjectsResult.Links != null && directoryObjects.GetDirectoryObjectsResult.Links.Length != 0 && array3.Length <= linksResultSize)
				{
					if (array3.Length == linksResultSize)
					{
						allLinksCollected = new bool?(false);
					}
					else
					{
						int num = array3.Length;
						int num2 = array3.Length + directoryObjects.GetDirectoryObjectsResult.Links.Length;
						int num3 = Math.Min(linksResultSize, num2);
						if (num2 > linksResultSize)
						{
							allLinksCollected = new bool?(false);
						}
						Array.Resize<DirectoryLink>(ref array3, num3);
						Array.Copy(directoryObjects.GetDirectoryObjectsResult.Links, 0, array3, num, num3 - num);
					}
				}
				if (directoryObjects.GetDirectoryObjectsResult.Errors != null && directoryObjects.GetDirectoryObjectsResult.Errors.Length != 0)
				{
					Array.Resize<DirectoryObjectError>(ref array4, array4.Length + directoryObjects.GetDirectoryObjectsResult.Errors.Length);
					directoryObjects.GetDirectoryObjectsResult.Errors.CopyTo(array4, array4.Length - directoryObjects.GetDirectoryObjectsResult.Errors.Length);
				}
				directoryObjectsAndLinks.NextPageToken = directoryObjects.GetDirectoryObjectsResult.NextPageToken;
				directoryObjectsAndLinks.More = directoryObjects.GetDirectoryObjectsResult.More;
			}
			directoryObjectsAndLinks.Objects = ((array2 != null && array2[0] != null) ? array2 : new DirectoryObject[0]);
			directoryObjectsAndLinks.Links = array3;
			directoryObjectsAndLinks.Errors = array4;
			return directoryObjectsAndLinks;
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x000847F7 File Offset: 0x000829F7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SyncService>(this);
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x000847FF File Offset: 0x000829FF
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.DisposeSyncServiceProxy();
			}
		}

		// Token: 0x06001E83 RID: 7811
		protected abstract DirectorySyncClient CreateService();

		// Token: 0x06001E84 RID: 7812 RVA: 0x0008480A File Offset: 0x00082A0A
		private void InitializeSyncServiceProxy()
		{
			if (this.SyncClient == null)
			{
				this.SyncClient = this.CreateService();
				this.SyncClient.ChannelFactory.Endpoint.EndpointBehaviors.Add(this);
			}
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x0008483C File Offset: 0x00082A3C
		private void DisposeSyncServiceProxy()
		{
			if (this.SyncClient != null)
			{
				IDisposable syncClient = this.SyncClient;
				if (syncClient != null)
				{
					syncClient.Dispose();
				}
				this.SyncClient = null;
			}
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x00084868 File Offset: 0x00082A68
		public byte[] GetNewCookieForAllObjectsTypes(string serviceInstanceId)
		{
			Type[] array = new Type[]
			{
				typeof(SyncUserSchema),
				typeof(SyncContactSchema),
				typeof(SyncGroupSchema),
				typeof(SyncForeignPrincipalSchema),
				typeof(SyncAccountSchema),
				typeof(SyncCompanySchema)
			};
			HashSet<string> hashSet = new HashSet<string>();
			HashSet<string> hashSet2 = new HashSet<string>();
			HashSet<string> hashSet3 = new HashSet<string>();
			HashSet<string> hashSet4 = new HashSet<string>();
			foreach (Type schemaType in array)
			{
				SyncObjectSchema syncObjectSchema = (SyncObjectSchema)ObjectSchema.GetInstance(schemaType);
				hashSet.Add(Enum.GetName(typeof(DirectoryObjectClass), syncObjectSchema.DirectoryObjectClass));
				ReadOnlyCollection<PropertyDefinition> allProperties = syncObjectSchema.AllProperties;
				foreach (PropertyDefinition propertyDefinition in allProperties)
				{
					SyncPropertyDefinition syncPropertyDefinition = propertyDefinition as SyncPropertyDefinition;
					if (syncPropertyDefinition != null && syncPropertyDefinition.IsForwardSync && !syncPropertyDefinition.IsNotInMsoDirectory && !string.IsNullOrEmpty(syncPropertyDefinition.MsoPropertyName))
					{
						if (syncPropertyDefinition.IsSyncLink)
						{
							hashSet3.Add(syncPropertyDefinition.MsoPropertyName);
						}
						else
						{
							hashSet2.Add(syncPropertyDefinition.MsoPropertyName);
							if (syncPropertyDefinition.IsAlwaysReturned)
							{
								hashSet4.Add(syncPropertyDefinition.MsoPropertyName);
							}
						}
					}
				}
			}
			NewCookie2Request request = new NewCookie2Request(0, serviceInstanceId, SyncOptions.None, hashSet.ToArray<string>(), hashSet2.ToArray<string>(), hashSet3.ToArray<string>(), hashSet4.ToArray<string>());
			NewCookie2Response newCookie2Response = this.SyncProxy.NewCookie2(request);
			return newCookie2Response.NewCookie2Result;
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x00084A2C File Offset: 0x00082C2C
		private byte[] GetMsoSyncCookie(string serviceInstanceId)
		{
			if (!this.msoSyncCookies.ContainsKey(serviceInstanceId))
			{
				this.msoSyncCookies.Add(serviceInstanceId, this.GetNewCookieForAllObjectsTypes(serviceInstanceId));
			}
			return this.msoSyncCookies[serviceInstanceId];
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x00084A5C File Offset: 0x00082C5C
		public void AfterReceiveReply(ref Message reply, object correlationState)
		{
			MessageBuffer messageBuffer = reply.CreateBufferedCopy(1048576);
			MemoryStream memoryStream = new MemoryStream();
			messageBuffer.WriteMessage(memoryStream);
			memoryStream.Position = 0L;
			StreamReader streamReader = new StreamReader(memoryStream);
			this.RawResponse = streamReader.ReadToEnd();
			streamReader.Close();
			memoryStream.Close();
			reply = messageBuffer.CreateMessage();
			messageBuffer.Close();
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x00084AB8 File Offset: 0x00082CB8
		public object BeforeSendRequest(ref Message request, IClientChannel channel)
		{
			return null;
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x00084ABB File Offset: 0x00082CBB
		public void AddBindingParameters(System.ServiceModel.Description.ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x00084ABD File Offset: 0x00082CBD
		public void ApplyClientBehavior(System.ServiceModel.Description.ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
			clientRuntime.ClientMessageInspectors.Add(this);
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x00084ACB File Offset: 0x00082CCB
		public void ApplyDispatchBehavior(System.ServiceModel.Description.ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x00084ACD File Offset: 0x00082CCD
		public void Validate(System.ServiceModel.Description.ServiceEndpoint endpoint)
		{
		}

		// Token: 0x0400192C RID: 6444
		private const int SchemaRevision = 0;

		// Token: 0x0400192D RID: 6445
		private readonly Dictionary<string, byte[]> msoSyncCookies = new Dictionary<string, byte[]>();
	}
}
