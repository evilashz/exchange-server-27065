using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Services.Client;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.WindowsAzure.ActiveDirectory;
using Microsoft.WindowsAzure.ActiveDirectory.GraphHelper;
using Microsoft.WindowsAzure.ActiveDirectoryV122;
using Microsoft.WindowsAzure.ActiveDirectoryV142;

namespace Microsoft.Exchange.Net.AAD
{
	// Token: 0x02000583 RID: 1411
	internal sealed class AADClient : IAadClient
	{
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x00028326 File Offset: 0x00026526
		// (set) Token: 0x0600127A RID: 4730 RVA: 0x0002832E File Offset: 0x0002652E
		public Dictionary<string, string> Headers { get; set; }

		// Token: 0x0600127B RID: 4731 RVA: 0x00028338 File Offset: 0x00026538
		public AADClient(string graphBaseURL, string tenantContextId, AADJWTToken token, GraphProxyVersions apiVersion = GraphProxyVersions.Version14)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("graphBaseURL", graphBaseURL);
			ArgumentValidator.ThrowIfNullOrEmpty("tenantContextId", tenantContextId);
			ArgumentValidator.ThrowIfNull("token", token);
			this.tenantContextId = tenantContextId;
			if (apiVersion == GraphProxyVersions.Version14)
			{
				this.Service = new Microsoft.WindowsAzure.ActiveDirectory.DirectoryDataService(graphBaseURL, tenantContextId, token);
			}
			else if (apiVersion == GraphProxyVersions.Version142)
			{
				this.ServiceV142 = new Microsoft.WindowsAzure.ActiveDirectoryV142.DirectoryDataService(graphBaseURL, tenantContextId, token);
			}
			else
			{
				this.ServiceV122 = new Microsoft.WindowsAzure.ActiveDirectoryV122.DirectoryDataService(graphBaseURL, tenantContextId, token);
			}
			this.version = apiVersion;
			this.Initialize();
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x000283BC File Offset: 0x000265BC
		public AADClient(string graphBaseURL, string tenantContextId, ICredentials credentials, GraphProxyVersions apiVersion = GraphProxyVersions.Version14)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("graphBaseURL", graphBaseURL);
			ArgumentValidator.ThrowIfNullOrEmpty("tenantContextId", tenantContextId);
			ArgumentValidator.ThrowIfNull("credentials", credentials);
			this.tenantContextId = tenantContextId;
			if (apiVersion == GraphProxyVersions.Version14)
			{
				this.Service = new Microsoft.WindowsAzure.ActiveDirectory.DirectoryDataService(graphBaseURL, tenantContextId, null);
				this.Service.Credentials = credentials;
			}
			else if (apiVersion == GraphProxyVersions.Version142)
			{
				this.ServiceV142 = new Microsoft.WindowsAzure.ActiveDirectoryV142.DirectoryDataService(graphBaseURL, tenantContextId, null);
				this.ServiceV142.Credentials = credentials;
			}
			else
			{
				this.ServiceV122 = new Microsoft.WindowsAzure.ActiveDirectoryV122.DirectoryDataService(graphBaseURL, tenantContextId, null);
				this.ServiceV122.Credentials = credentials;
			}
			this.version = apiVersion;
			this.Initialize();
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x00028461 File Offset: 0x00026661
		// (set) Token: 0x0600127E RID: 4734 RVA: 0x00028498 File Offset: 0x00026698
		public int Timeout
		{
			get
			{
				if (this.version == GraphProxyVersions.Version14)
				{
					return this.Service.Timeout;
				}
				if (this.version == GraphProxyVersions.Version142)
				{
					return this.ServiceV142.Timeout;
				}
				return this.ServiceV122.Timeout;
			}
			set
			{
				if (this.version == GraphProxyVersions.Version14)
				{
					this.Service.Timeout = value;
					return;
				}
				if (this.version == GraphProxyVersions.Version142)
				{
					this.ServiceV142.Timeout = value;
					return;
				}
				this.ServiceV122.Timeout = value;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x000284D2 File Offset: 0x000266D2
		// (set) Token: 0x06001280 RID: 4736 RVA: 0x000284DA File Offset: 0x000266DA
		internal Microsoft.WindowsAzure.ActiveDirectory.DirectoryDataService Service { get; private set; }

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x000284E3 File Offset: 0x000266E3
		// (set) Token: 0x06001282 RID: 4738 RVA: 0x000284EB File Offset: 0x000266EB
		internal Microsoft.WindowsAzure.ActiveDirectoryV122.DirectoryDataService ServiceV122 { get; private set; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x000284F4 File Offset: 0x000266F4
		// (set) Token: 0x06001284 RID: 4740 RVA: 0x000284FC File Offset: 0x000266FC
		internal Microsoft.WindowsAzure.ActiveDirectoryV142.DirectoryDataService ServiceV142 { get; private set; }

		// Token: 0x06001285 RID: 4741 RVA: 0x00028508 File Offset: 0x00026708
		public string CreateGroup(string displayName, string alias, string description, bool isPublic)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("displayName", displayName);
			ArgumentValidator.ThrowIfNullOrEmpty("alias", alias);
			Microsoft.WindowsAzure.ActiveDirectory.Group group = new Microsoft.WindowsAzure.ActiveDirectory.Group();
			group.securityEnabled = new bool?(false);
			group.mailEnabled = new bool?(true);
			group.groupType = "Unified";
			group.mailNickname = alias;
			group.displayName = displayName;
			group.isPublic = new bool?(isPublic);
			if (!string.IsNullOrEmpty(description))
			{
				group.description = description;
			}
			this.Service.AddTogroups(group);
			this.SaveChanges("CreateGroup");
			return group.objectId;
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0002859C File Offset: 0x0002679C
		public void DeleteGroup(string objectId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("objectId", objectId);
			Microsoft.WindowsAzure.ActiveDirectory.Group group = new Microsoft.WindowsAzure.ActiveDirectory.Group();
			group.objectId = objectId;
			this.Service.AttachTo("groups", group);
			try
			{
				this.Service.DeleteObject(group);
				this.SaveChanges("DeleteGroup");
			}
			finally
			{
				this.Service.Detach(group);
			}
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0002860C File Offset: 0x0002680C
		public void UpdateGroup(string objectId, string description = null, string[] exchangeResources = null, string displayName = null, bool? isPublic = null)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("objectId", objectId);
			Microsoft.WindowsAzure.ActiveDirectory.Group group = this.GetGroup(objectId, true);
			if (group != null)
			{
				if (description != null)
				{
					group.description = ((description == string.Empty) ? null : description);
				}
				if (exchangeResources != null)
				{
					group.exchangeResources = new Collection<string>(exchangeResources);
				}
				if (displayName != null)
				{
					group.displayName = displayName;
				}
				if (isPublic != null)
				{
					group.isPublic = new bool?(isPublic.Value);
				}
				this.Service.UpdateObject(group);
				this.SaveChanges("UpdateGroup", 8);
			}
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x000286C0 File Offset: 0x000268C0
		public AADClient.LinkResult[] AddMembers(string groupObjectId, params string[] userObjectIds)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("groupObjectId", groupObjectId);
			return this.BatchLinkOperation(userObjectIds, delegate(string[] batchUsers)
			{
				this.UpdateLinks("AddMembers", groupObjectId, "members", true, batchUsers);
			});
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0002872C File Offset: 0x0002692C
		public AADClient.LinkResult[] RemoveMembers(string groupObjectId, params string[] userObjectIds)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("groupObjectId", groupObjectId);
			return this.BatchLinkOperation(userObjectIds, delegate(string[] batchUsers)
			{
				this.UpdateLinks("RemoveMembers", groupObjectId, "members", false, batchUsers);
			});
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00028798 File Offset: 0x00026998
		public AADClient.LinkResult[] AddOwners(string groupObjectId, params string[] userObjectIds)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("groupObjectId", groupObjectId);
			return this.BatchLinkOperation(userObjectIds, delegate(string[] batchUsers)
			{
				this.UpdateLinks("AddOwners", groupObjectId, "owners", true, batchUsers);
			});
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x00028804 File Offset: 0x00026A04
		public AADClient.LinkResult[] RemoveOwners(string groupObjectId, params string[] userObjectIds)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("groupObjectId", groupObjectId);
			return this.BatchLinkOperation(userObjectIds, delegate(string[] batchUsers)
			{
				this.UpdateLinks("RemoveOwners", groupObjectId, "owners", false, batchUsers);
			});
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x00028870 File Offset: 0x00026A70
		public AADClient.LinkResult[] AddAllowAccessTo(string groupObjectId, params string[] groupObjectIds)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("groupObjectId", groupObjectId);
			return this.BatchLinkOperation(groupObjectIds, delegate(string[] batchGroups)
			{
				this.UpdateLinks("AddAllowAccessTo", groupObjectId, "allowAccessTo", true, batchGroups);
			});
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x000288DC File Offset: 0x00026ADC
		public AADClient.LinkResult[] RemoveAllowAccessTo(string groupObjectId, params string[] groupObjectIds)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("groupObjectId", groupObjectId);
			return this.BatchLinkOperation(groupObjectIds, delegate(string[] batchGroups)
			{
				this.UpdateLinks("RemoveAllowAccessTo", groupObjectId, "allowAccessTo", false, batchGroups);
			});
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00028948 File Offset: 0x00026B48
		public AADClient.LinkResult[] AddPendingMembers(string groupObjectId, params string[] userObjectIds)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("groupObjectId", groupObjectId);
			return this.BatchLinkOperation(userObjectIds, delegate(string[] batchUsers)
			{
				this.UpdateLinks("AddPendingMembers", groupObjectId, "pendingMembers", true, batchUsers);
			});
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x000289B4 File Offset: 0x00026BB4
		public AADClient.LinkResult[] RemovePendingMembers(string groupObjectId, params string[] userObjectIds)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("groupObjectId", groupObjectId);
			return this.BatchLinkOperation(userObjectIds, delegate(string[] batchUsers)
			{
				this.UpdateLinks("RemovePendingMembers", groupObjectId, "pendingMembers", false, batchUsers);
			});
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00028A14 File Offset: 0x00026C14
		public string[] GetGroupMembership(string userObjectId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("userObjectId", userObjectId);
			string text = "users/" + userObjectId + "/memberOf";
			DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.Group> query = this.Service.CreateQuery<Microsoft.WindowsAzure.ActiveDirectory.Group>(text);
			List<string> list = new List<string>(8);
			bool ignoreMissingProperties = this.Service.IgnoreMissingProperties;
			try
			{
				this.Service.IgnoreMissingProperties = true;
				DataServiceQueryContinuation<Microsoft.WindowsAzure.ActiveDirectory.Group> continuation;
				for (QueryOperationResponse<Microsoft.WindowsAzure.ActiveDirectory.Group> queryOperationResponse = this.ExecuteSearchQuery<Microsoft.WindowsAzure.ActiveDirectory.Group>(query, false); queryOperationResponse != null; queryOperationResponse = this.ExecuteSearchQuery<Microsoft.WindowsAzure.ActiveDirectory.Group>(continuation))
				{
					list.AddRange(from @group in queryOperationResponse
					where @group.groupType == "Unified"
					select @group.objectId);
					continuation = queryOperationResponse.GetContinuation();
					if (continuation == null)
					{
						break;
					}
				}
			}
			finally
			{
				this.Service.IgnoreMissingProperties = ignoreMissingProperties;
			}
			return list.ToArray();
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00028B04 File Offset: 0x00026D04
		public bool IsUserMemberOfGroup(string userObjectId, string groupObjectId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("userObjectId", userObjectId);
			ArgumentValidator.ThrowIfNullOrEmpty("groupObjectId", groupObjectId);
			string text = "users/" + userObjectId + "/memberOf/" + groupObjectId;
			DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.Group> query = this.Service.CreateQuery<Microsoft.WindowsAzure.ActiveDirectory.Group>(text);
			QueryOperationResponse<Microsoft.WindowsAzure.ActiveDirectory.Group> queryOperationResponse = this.ExecuteSearchQuery<Microsoft.WindowsAzure.ActiveDirectory.Group>(query, false);
			bool flag = queryOperationResponse != null && queryOperationResponse.Count<Microsoft.WindowsAzure.ActiveDirectory.Group>() != 0;
			AADClient.Tracer.TraceDebug<string, string, bool>((long)this.GetHashCode(), "User: {0}, Group: {1}, isMember: {2}", userObjectId, groupObjectId, flag);
			return flag;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00028B84 File Offset: 0x00026D84
		public string GetUserObjectId(string userPrincipalName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("userPrincipalName", userPrincipalName);
			DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectoryV122.User> query = (DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectoryV122.User>)(from user in this.ServiceV122.users
			where string.Equals(user.userPrincipalName, userPrincipalName)
			select user);
			QueryOperationResponse<Microsoft.WindowsAzure.ActiveDirectoryV122.User> queryOperationResponse = this.ExecuteQuery<Microsoft.WindowsAzure.ActiveDirectoryV122.User>(query, false);
			if (queryOperationResponse == null)
			{
				AADClient.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Did not find User with userPrincipalName {0}", userPrincipalName);
				return null;
			}
			List<Microsoft.WindowsAzure.ActiveDirectoryV122.User> list = queryOperationResponse.ToList<Microsoft.WindowsAzure.ActiveDirectoryV122.User>();
			if (list.Count <= 0)
			{
				return string.Empty;
			}
			return list.FirstOrDefault<Microsoft.WindowsAzure.ActiveDirectoryV122.User>().objectId;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00028C94 File Offset: 0x00026E94
		internal string CreateDevice(string displayName, string easID, Guid deviceID, string userPrincipalName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("displayName", displayName);
			ArgumentValidator.ThrowIfNullOrEmpty("easID", easID);
			ArgumentValidator.ThrowIfNullOrEmpty("userPrincipalName", userPrincipalName);
			ArgumentValidator.ThrowIfNull("deviceID", deviceID);
			Microsoft.WindowsAzure.ActiveDirectoryV122.Device device = new Microsoft.WindowsAzure.ActiveDirectoryV122.Device();
			device.accountEnabled = new bool?(true);
			device.alternativeSecurityIds = new Collection<Microsoft.WindowsAzure.ActiveDirectoryV122.AlternativeSecurityId>();
			device.alternativeSecurityIds.Add(new Microsoft.WindowsAzure.ActiveDirectoryV122.AlternativeSecurityId
			{
				key = Guid.NewGuid().ToByteArray(),
				type = new int?(2),
				identityProvider = null
			});
			device.deviceId = new Guid?(deviceID);
			device.deviceOSType = "iOS";
			device.displayName = displayName;
			device.deviceOSVersion = "2.0";
			Microsoft.WindowsAzure.ActiveDirectoryV122.User user = (from x in this.ServiceV122.users
			where x.userPrincipalName == userPrincipalName
			select x).ToList<Microsoft.WindowsAzure.ActiveDirectoryV122.User>().FirstOrDefault<Microsoft.WindowsAzure.ActiveDirectoryV122.User>();
			if (user == null)
			{
				return string.Format("user not found. UserPrincipalName {0}", userPrincipalName);
			}
			device.exchangeActiveSyncId = new Collection<string>();
			device.exchangeActiveSyncId.Add(string.Format("eas:{0}:{1}:{2}", easID, user.objectId, ExDateTime.UtcNow.ToShortDateString()));
			this.ServiceV122.AddObject("devices", device);
			this.SaveChanges("CreateDevice");
			this.ServiceV122.AddLink(device, "registeredOwners", user);
			this.ServiceV122.AddLink(device, "registeredUsers", user);
			this.SaveChanges("UpdateDeviceOwner");
			return device.objectId;
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00028E8C File Offset: 0x0002708C
		internal string UpdateDevice(Guid deviceId, Collection<string> easIds, bool? isManaged, bool? isCompliant, bool? accountEnabled)
		{
			ArgumentValidator.ThrowIfNull("deviceId", deviceId);
			Microsoft.WindowsAzure.ActiveDirectoryV122.Device device = (from x in this.ServiceV122.devices
			where x.deviceId == (Guid?)deviceId
			select x).ToList<Microsoft.WindowsAzure.ActiveDirectoryV122.Device>().FirstOrDefault<Microsoft.WindowsAzure.ActiveDirectoryV122.Device>();
			if (device != null)
			{
				if (easIds != null)
				{
					device.exchangeActiveSyncId = easIds;
				}
				if (isCompliant != null)
				{
					device.isCompliant = isCompliant;
				}
				if (isManaged != null)
				{
					device.isManaged = isManaged;
				}
				if (accountEnabled != null)
				{
					device.accountEnabled = accountEnabled;
				}
				this.ServiceV122.UpdateObject(device);
				this.SaveChanges("UpdateDevice");
				return device.objectId;
			}
			return string.Format("Device with id {0} not found", device.ToString());
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00028FB8 File Offset: 0x000271B8
		public string EvaluateAuthPolicy(string easId, string userObjectId, bool isSupportedPlatform)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("easId", easId);
			ArgumentValidator.ThrowIfNullOrEmpty("userObjectId", userObjectId);
			string text = this.ServiceV142.BaseUri.ToString() + "/users/" + userObjectId + "/evaluateAuthPolicy";
			AADClient.Tracer.TraceDebug<string, string, bool>((long)this.GetHashCode(), "Evaluate Auth Policy for with serviceInfoQUery {0}, easID:{1}, IsSupported:{2}", text, easId, isSupportedPlatform);
			QueryOperationResponse<ICollection<string>> queryOperationResponse = this.ServiceV142.Execute<ICollection<string>>(new Uri(text), "POST", true, new OperationParameter[]
			{
				new BodyOperationParameter("applicationIdentifier", "00000002-0000-0ff1-ce00-000000000000"),
				new BodyOperationParameter("exchangeActiveSyncId", easId),
				new BodyOperationParameter("platform", isSupportedPlatform ? "eas_supported" : "eas_unsupported")
			}) as QueryOperationResponse<ICollection<string>>;
			if (queryOperationResponse == null)
			{
				AADClient.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "Could not Evaluate Auth Policy for user {0}, device {1}", userObjectId, easId);
				return null;
			}
			ICollection<string> collection = queryOperationResponse.ToList<ICollection<string>>().FirstOrDefault<ICollection<string>>();
			if (collection == null)
			{
				AADClient.Tracer.TraceDebug((long)this.GetHashCode(), "response List is empty");
				return null;
			}
			AADClient.Tracer.TraceDebug(this.GetHashCode(), 0L, "EvaluateAuthPolicy Response for user {0}, Device {1}. ResponseCount:{3}, Value:{4}", new object[]
			{
				userObjectId,
				easId,
				collection.Count<string>(),
				collection.FirstOrDefault<string>()
			});
			return collection.FirstOrDefault<string>();
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00029134 File Offset: 0x00027334
		public List<AadDevice> GetUserDevicesWithEasID(string easId, string userObjectId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("easId", easId);
			ArgumentValidator.ThrowIfNullOrEmpty("userObjectId", userObjectId);
			string pattern = string.Format(CultureInfo.InvariantCulture, "eas:{0}:{1}:", new object[]
			{
				easId,
				userObjectId
			});
			DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectoryV122.Device> query = (DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectoryV122.Device>)(from device in this.ServiceV122.devices
			where device.exchangeActiveSyncId.Any((string id) => id.StartsWith(pattern))
			select device);
			QueryOperationResponse<Microsoft.WindowsAzure.ActiveDirectoryV122.Device> queryOperationResponse = this.ExecuteQuery<Microsoft.WindowsAzure.ActiveDirectoryV122.Device>(query, false);
			if (queryOperationResponse == null)
			{
				AADClient.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "Did not find Device with easID {0} and userObjectId {1)", easId, userObjectId);
				return null;
			}
			List<AadDevice> aadDevices = new List<AadDevice>();
			queryOperationResponse.ToList<Microsoft.WindowsAzure.ActiveDirectoryV122.Device>().ForEach(delegate(Microsoft.WindowsAzure.ActiveDirectoryV122.Device s)
			{
				aadDevices.Add(this.GetAadDevice(s, easId, userObjectId));
			});
			return aadDevices;
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x000292E8 File Offset: 0x000274E8
		public string[] GetServiceInfo(string serviceInstance)
		{
			string text = "tenantDetails/" + this.tenantContextId + "/serviceInfo";
			DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.ServiceInfo> query = this.Service.CreateQuery<Microsoft.WindowsAzure.ActiveDirectory.ServiceInfo>(text);
			QueryOperationResponse<Microsoft.WindowsAzure.ActiveDirectory.ServiceInfo> queryOperationResponse = this.ExecuteSearchQuery<Microsoft.WindowsAzure.ActiveDirectory.ServiceInfo>(query, false);
			if (queryOperationResponse == null)
			{
				AADClient.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Did not find ServiceInfo for tenant {0}", this.tenantContextId);
				return null;
			}
			foreach (Microsoft.WindowsAzure.ActiveDirectory.ServiceInfo serviceInfo in queryOperationResponse)
			{
				if (serviceInfo.serviceInstance.StartsWith(serviceInstance, StringComparison.OrdinalIgnoreCase) && serviceInfo.serviceElements != null && serviceInfo.serviceElements.Count > 0)
				{
					AADClient.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Found serviceInstance: {0}", serviceInstance);
					return serviceInfo.serviceElements.ToArray<string>();
				}
			}
			AADClient.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Did not find serviceInstance: {0}", serviceInstance);
			return null;
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00029414 File Offset: 0x00027614
		public string GetDefaultDomain()
		{
			DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.TenantDetail> query = (DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.TenantDetail>)(from t in this.Service.tenantDetails
			select t);
			QueryOperationResponse<Microsoft.WindowsAzure.ActiveDirectory.TenantDetail> queryOperationResponse = this.ExecuteSearchQuery<Microsoft.WindowsAzure.ActiveDirectory.TenantDetail>(query, false);
			if (queryOperationResponse != null)
			{
				Collection<Microsoft.WindowsAzure.ActiveDirectory.VerifiedDomain> verifiedDomains = queryOperationResponse.ToList<Microsoft.WindowsAzure.ActiveDirectory.TenantDetail>().FirstOrDefault<Microsoft.WindowsAzure.ActiveDirectory.TenantDetail>().verifiedDomains;
				if (verifiedDomains != null && verifiedDomains.Count > 0)
				{
					return (from domain in verifiedDomains
					where domain.@default == true
					select domain.name).FirstOrDefault<string>();
				}
			}
			return null;
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x000294E0 File Offset: 0x000276E0
		public bool IsAliasUnique(string alias)
		{
			DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.Group> query = (DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.Group>)(from g in this.Service.groups
			where g.mailNickname == alias
			select g);
			QueryOperationResponse<Microsoft.WindowsAzure.ActiveDirectory.Group> queryOperationResponse = this.ExecuteSearchQuery<Microsoft.WindowsAzure.ActiveDirectory.Group>(query, false);
			return queryOperationResponse == null || queryOperationResponse.Count<Microsoft.WindowsAzure.ActiveDirectory.Group>() == 0;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00029598 File Offset: 0x00027798
		public bool IsUserEnabled(string userSmtpAddress)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("userSmtpAddress", userSmtpAddress);
			string userProxyAddress = "SMTP:" + userSmtpAddress;
			Microsoft.WindowsAzure.ActiveDirectory.User user;
			DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.User> query = (DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.User>)(from user in this.Service.users
			where string.Equals(user.userPrincipalName, userSmtpAddress) || user.proxyAddresses.Any((string proxyAddress) => string.Equals(proxyAddress, userProxyAddress))
			select user);
			QueryOperationResponse<Microsoft.WindowsAzure.ActiveDirectory.User> queryOperationResponse = this.ExecuteSearchQuery<Microsoft.WindowsAzure.ActiveDirectory.User>(query, false);
			if (queryOperationResponse == null)
			{
				AADClient.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Did not find User with userPrincipalName or proxy addresses: {0}", userSmtpAddress);
				return false;
			}
			using (IEnumerator<Microsoft.WindowsAzure.ActiveDirectory.User> enumerator = queryOperationResponse.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					user = enumerator.Current;
					if (user.accountEnabled != null && user.accountEnabled.Value)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x000297A4 File Offset: 0x000279A4
		internal QueryOperationResponse<T> ExecuteSearchQuery<T>(DataServiceQuery<T> query, bool throwIfNotFound = false) where T : Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject
		{
			QueryOperationResponse<T> result;
			try
			{
				AADClient.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Executing query: {0}", query.ToString());
				result = (query.Execute() as QueryOperationResponse<T>);
			}
			catch (DataServiceQueryException ex)
			{
				if (throwIfNotFound || !AADClient.IsRecordNotFoundException(ex))
				{
					AADClient.Tracer.TraceError<DataServiceQueryException>((long)this.GetHashCode(), "ExecuteSearchQuery failed: {0}", ex);
					throw new AADDataException(ex);
				}
				AADClient.Tracer.TraceDebug((long)this.GetHashCode(), "Record not found");
				result = null;
			}
			catch (DataServiceTransportException ex2)
			{
				AADClient.Tracer.TraceError<DataServiceTransportException>((long)this.GetHashCode(), "ExecuteSearchQuery failed: {0}", ex2);
				throw new AADTransportException(ex2);
			}
			return result;
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0002985C File Offset: 0x00027A5C
		internal QueryOperationResponse<T> ExecuteQuery<T>(DataServiceQuery<T> query, bool throwIfNotFound = false) where T : Microsoft.WindowsAzure.ActiveDirectoryV122.DirectoryObject
		{
			QueryOperationResponse<T> result;
			try
			{
				AADClient.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Executing query: {0}", query.ToString());
				result = (query.Execute() as QueryOperationResponse<T>);
			}
			catch (DataServiceQueryException ex)
			{
				if (throwIfNotFound || !AADClient.IsRecordNotFoundException(ex))
				{
					AADClient.Tracer.TraceError<DataServiceQueryException>((long)this.GetHashCode(), "ExecuteQuery failed: {0}", ex);
					throw new AADDataException(ex);
				}
				AADClient.Tracer.TraceDebug((long)this.GetHashCode(), "Record not found");
				result = null;
			}
			catch (DataServiceTransportException ex2)
			{
				AADClient.Tracer.TraceError<DataServiceTransportException>((long)this.GetHashCode(), "ExecuteQuery failed: {0}", ex2);
				throw new AADTransportException(ex2);
			}
			return result;
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00029914 File Offset: 0x00027B14
		internal QueryOperationResponse<T> ExecuteSearchQuery<T>(DataServiceQueryContinuation<T> continuation) where T : Microsoft.WindowsAzure.ActiveDirectory.DirectoryObject
		{
			QueryOperationResponse<T> result;
			try
			{
				AADClient.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Executing continuation query: {0}", continuation.ToString());
				result = this.Service.Execute<T>(continuation);
			}
			catch (DataServiceQueryException ex)
			{
				AADClient.Tracer.TraceError<DataServiceQueryException>((long)this.GetHashCode(), "ExecuteSearchQuery failed: {0}", ex);
				throw new AADDataException(ex);
			}
			catch (DataServiceTransportException ex2)
			{
				AADClient.Tracer.TraceError<DataServiceTransportException>((long)this.GetHashCode(), "ExecuteSearchQuery failed: {0}", ex2);
				throw new AADTransportException(ex2);
			}
			return result;
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x000299B0 File Offset: 0x00027BB0
		internal Microsoft.WindowsAzure.ActiveDirectory.Group GetGroup(string objectId, bool throwIfNotFound = true)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("objectId", objectId);
			DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.Group> query = (DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.Group>)(from g in this.Service.groups
			where g.objectId == objectId
			select g);
			QueryOperationResponse<Microsoft.WindowsAzure.ActiveDirectory.Group> queryOperationResponse = this.ExecuteSearchQuery<Microsoft.WindowsAzure.ActiveDirectory.Group>(query, throwIfNotFound);
			if (queryOperationResponse != null)
			{
				return queryOperationResponse.ToList<Microsoft.WindowsAzure.ActiveDirectory.Group>().FirstOrDefault<Microsoft.WindowsAzure.ActiveDirectory.Group>();
			}
			return null;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00029CE4 File Offset: 0x00027EE4
		internal IEnumerable<Microsoft.WindowsAzure.ActiveDirectory.Group> GetGroups()
		{
			DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.Group> query = (DataServiceQuery<Microsoft.WindowsAzure.ActiveDirectory.Group>)(from g in this.Service.groups
			where g.groupType == "Unified"
			select g);
			DataServiceQueryContinuation<Microsoft.WindowsAzure.ActiveDirectory.Group> continuation;
			for (QueryOperationResponse<Microsoft.WindowsAzure.ActiveDirectory.Group> response = this.ExecuteSearchQuery<Microsoft.WindowsAzure.ActiveDirectory.Group>(query, false); response != null; response = this.ExecuteSearchQuery<Microsoft.WindowsAzure.ActiveDirectory.Group>(continuation))
			{
				foreach (Microsoft.WindowsAzure.ActiveDirectory.Group group in response)
				{
					yield return group;
				}
				continuation = response.GetContinuation();
				if (continuation == null)
				{
					break;
				}
			}
			yield break;
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00029D04 File Offset: 0x00027F04
		private static bool IsRecordNotFoundException(Exception e)
		{
			while (e != null)
			{
				DataServiceClientException ex = e as DataServiceClientException;
				if (ex != null)
				{
					return ex.StatusCode == 404;
				}
				e = e.InnerException;
			}
			return false;
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00029D38 File Offset: 0x00027F38
		private void Initialize()
		{
			this.Headers = new Dictionary<string, string>();
			if (this.version == GraphProxyVersions.Version14)
			{
				this.Service.SendingRequest2 += this.OnSendingRequest2;
				this.Service.WritingEntity += this.OnWritingEntity;
				this.Service.ReceivingResponse += this.OnReceivingResponse;
				this.Service.ReadingEntity += this.OnReadingEntity;
				return;
			}
			if (this.version == GraphProxyVersions.Version142)
			{
				this.ServiceV142.SendingRequest2 += this.OnSendingRequest2;
				this.ServiceV142.WritingEntity += this.OnWritingEntity;
				this.ServiceV142.ReceivingResponse += this.OnReceivingResponse;
				this.ServiceV142.ReadingEntity += this.OnReadingEntity;
				return;
			}
			this.ServiceV122.SendingRequest2 += this.OnSendingRequest2;
			this.ServiceV122.WritingEntity += this.OnWritingEntity;
			this.ServiceV122.ReceivingResponse += this.OnReceivingResponse;
			this.ServiceV122.ReadingEntity += this.OnReadingEntity;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00029E78 File Offset: 0x00028078
		private void UpdateLinks(string action, string groupObjectId, string sourceProperty, bool addLink, params string[] objectIds)
		{
			Microsoft.WindowsAzure.ActiveDirectory.Group group = new Microsoft.WindowsAzure.ActiveDirectory.Group();
			group.objectId = groupObjectId;
			this.Service.AttachTo("groups", group);
			Microsoft.WindowsAzure.ActiveDirectory.User[] array = new Microsoft.WindowsAzure.ActiveDirectory.User[objectIds.Length];
			try
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new Microsoft.WindowsAzure.ActiveDirectory.User();
					array[i].objectId = objectIds[i];
					this.Service.AttachTo("users", array[i]);
					if (addLink)
					{
						this.Service.AddLink(group, sourceProperty, array[i]);
					}
					else
					{
						this.Service.DeleteLink(group, sourceProperty, array[i]);
					}
				}
				this.Service.UsePermissiveReferenceUpdates = true;
				this.SaveChanges(action, (array.Length > 1) ? 1 : 0);
			}
			finally
			{
				this.Service.UsePermissiveReferenceUpdates = false;
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j] != null)
					{
						this.Service.Detach(array[j]);
					}
				}
				this.Service.Detach(group);
			}
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00029F74 File Offset: 0x00028174
		private AADClient.LinkResult[] BatchLinkOperation(string[] links, Action<string[]> action)
		{
			List<AADClient.LinkResult> list = new List<AADClient.LinkResult>(2);
			if (links != null && links.Length > 0)
			{
				int num;
				for (int i = 0; i < links.Length; i += num)
				{
					num = Math.Min(20, links.Length - i);
					string[] subArray = AADClient.GetSubArray<string>(links, i, num);
					AADException ex = null;
					try
					{
						action(subArray);
					}
					catch (AADException ex2)
					{
						ex = ex2;
					}
					if (ex != null)
					{
						AADClient.Tracer.TraceError<AADException>((long)this.GetHashCode(), "Batch action failed with exception: {0}", ex);
						if (num == 1)
						{
							list.Add(new AADClient.LinkResult
							{
								FailedLink = subArray[0],
								Exception = ex
							});
						}
						else
						{
							foreach (string text in subArray)
							{
								try
								{
									action(new string[]
									{
										text
									});
								}
								catch (AADException ex3)
								{
									list.Add(new AADClient.LinkResult
									{
										FailedLink = text,
										Exception = ex3
									});
									AADClient.Tracer.TraceError<string, AADException>((long)this.GetHashCode(), "Batch action failed for {0} with exception: {1}", text, ex3);
								}
							}
						}
					}
				}
			}
			if (!list.Any<AADClient.LinkResult>())
			{
				return null;
			}
			return list.ToArray();
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0002A0BC File Offset: 0x000282BC
		private static T[] GetSubArray<T>(T[] array, int start, int length)
		{
			if (start == 0 && length == array.Length)
			{
				return array;
			}
			T[] array2 = new T[length];
			Array.Copy(array, start, array2, 0, length);
			return array2;
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0002A0E8 File Offset: 0x000282E8
		private void SaveChanges(string action)
		{
			if (this.version == GraphProxyVersions.Version14)
			{
				this.SaveChanges(action, this.Service.SaveChangesDefaultOptions);
				return;
			}
			if (this.version == GraphProxyVersions.Version142)
			{
				this.SaveChanges(action, this.ServiceV142.SaveChangesDefaultOptions);
				return;
			}
			this.SaveChanges(action, this.ServiceV122.SaveChangesDefaultOptions);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0002A140 File Offset: 0x00028340
		private void SaveChanges(string action, SaveChangesOptions options)
		{
			AADClient.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Calling SaveChanges for {0}", action);
			try
			{
				if (this.version == GraphProxyVersions.Version14)
				{
					this.Service.SaveChanges(options);
				}
				else if (this.version == GraphProxyVersions.Version142)
				{
					this.ServiceV142.SaveChanges(options);
				}
				else
				{
					this.ServiceV122.SaveChanges(options);
				}
			}
			catch (DataServiceRequestException ex)
			{
				AADClient.Tracer.TraceError<DataServiceRequestException>((long)this.GetHashCode(), "SaveChanges failed: {0}", ex);
				throw new AADDataException(ex);
			}
			catch (DataServiceTransportException ex2)
			{
				AADClient.Tracer.TraceError<DataServiceTransportException>((long)this.GetHashCode(), "SaveChanges failed: {0}", ex2);
				throw new AADTransportException(ex2);
			}
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0002A200 File Offset: 0x00028400
		private void OnSendingRequest2(object sender, SendingRequest2EventArgs args)
		{
			foreach (KeyValuePair<string, string> keyValuePair in this.Headers)
			{
				args.RequestMessage.SetHeader(keyValuePair.Key, keyValuePair.Value);
			}
			if (!AADClient.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Url: ");
			stringBuilder.AppendLine(args.RequestMessage.Url.ToString());
			stringBuilder.Append("Method: ");
			stringBuilder.AppendLine(args.RequestMessage.Method);
			stringBuilder.AppendLine("Headers:");
			foreach (KeyValuePair<string, string> keyValuePair2 in args.RequestMessage.Headers)
			{
				stringBuilder.Append(keyValuePair2.Key);
				stringBuilder.Append(": ");
				stringBuilder.AppendLine(keyValuePair2.Value);
			}
			AADClient.Tracer.TraceDebug<string>((long)this.GetHashCode(), "AADClient.OnSendingRequest2: {0}", stringBuilder.ToString());
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0002A34C File Offset: 0x0002854C
		private void OnWritingEntity(object sender, ReadingWritingEntityEventArgs args)
		{
			AADClient.Tracer.TraceDebug<XElement>((long)this.GetHashCode(), "AADClient.OnWritingEntity: {0}", args.Data);
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0002A36C File Offset: 0x0002856C
		private void OnReceivingResponse(object sender, ReceivingResponseEventArgs args)
		{
			if (!AADClient.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("StatusCode: ");
			stringBuilder.AppendLine(args.ResponseMessage.StatusCode.ToString());
			stringBuilder.AppendLine("Headers:");
			foreach (KeyValuePair<string, string> keyValuePair in args.ResponseMessage.Headers)
			{
				stringBuilder.Append(keyValuePair.Key);
				stringBuilder.Append(": ");
				stringBuilder.AppendLine(keyValuePair.Value);
			}
			AADClient.Tracer.TraceDebug<string>((long)this.GetHashCode(), "AADClient.OnReceivingResponse: {0}", stringBuilder.ToString());
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x0002A444 File Offset: 0x00028644
		private void OnReadingEntity(object sender, ReadingWritingEntityEventArgs args)
		{
			AADClient.Tracer.TraceDebug<XElement>((long)this.GetHashCode(), "AADClient.OnReadingEntity: {0}", args.Data);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0002A48C File Offset: 0x0002868C
		private AadDevice GetAadDevice(Microsoft.WindowsAzure.ActiveDirectoryV122.Device device, string easDeviceId, string userObjectId)
		{
			AadDevice aadDevice = new AadDevice();
			aadDevice.AccountEnabled = device.accountEnabled;
			aadDevice.DeviceId = device.deviceId;
			aadDevice.DisplayName = device.displayName;
			aadDevice.ExchangeActiveSyncIds = device.exchangeActiveSyncId.ToList<string>();
			aadDevice.IsCompliant = device.isCompliant;
			aadDevice.IsManaged = device.isManaged;
			string text = (from s in device.exchangeActiveSyncId
			where s.StartsWith(string.Format("eas:{0}:{1}:", easDeviceId, userObjectId), StringComparison.OrdinalIgnoreCase)
			select s).First<string>();
			AADClient.Tracer.TraceDebug<string>((long)this.GetHashCode(), "AADClient.GetAadDevice: exchangeActiveSyncId {0}", text);
			aadDevice.LastUpdated = DateTime.ParseExact(text.Substring(text.LastIndexOf(':') + 1), "yyyyMMddTHHmmss", CultureInfo.InvariantCulture);
			return aadDevice;
		}

		// Token: 0x04001843 RID: 6211
		private const string UnifiedGroupType = "Unified";

		// Token: 0x04001844 RID: 6212
		private const string MembersProperty = "members";

		// Token: 0x04001845 RID: 6213
		private const string OwnersProperty = "owners";

		// Token: 0x04001846 RID: 6214
		private const string AllowAccessToProperty = "allowAccessTo";

		// Token: 0x04001847 RID: 6215
		private const string PendingMembersProperty = "pendingMembers";

		// Token: 0x04001848 RID: 6216
		private const string AppIdentifierParameter = "applicationIdentifier";

		// Token: 0x04001849 RID: 6217
		private const string EasIDParameter = "exchangeActiveSyncId";

		// Token: 0x0400184A RID: 6218
		private const string PlatformPamaneter = "platform";

		// Token: 0x0400184B RID: 6219
		private const string ApplicationId = "00000002-0000-0ff1-ce00-000000000000";

		// Token: 0x0400184C RID: 6220
		private const string SupportedPlatformIdentifier = "eas_supported";

		// Token: 0x0400184D RID: 6221
		private const string UnSupportedPlatformIdentifier = "eas_unsupported";

		// Token: 0x0400184E RID: 6222
		private const string GroupsEntitySetName = "groups";

		// Token: 0x0400184F RID: 6223
		private const string UsersEntitySetName = "users";

		// Token: 0x04001850 RID: 6224
		private const string ExchangeActiveSyncIdFormat = "eas:{0}:{1}:";

		// Token: 0x04001851 RID: 6225
		private const char AadEasIdSeparator = ':';

		// Token: 0x04001852 RID: 6226
		private static readonly Trace Tracer = ExTraceGlobals.AADClientTracer;

		// Token: 0x04001853 RID: 6227
		private readonly string tenantContextId;

		// Token: 0x04001854 RID: 6228
		private readonly GraphProxyVersions version;

		// Token: 0x02000584 RID: 1412
		internal class LinkResult
		{
			// Token: 0x170003AB RID: 939
			// (get) Token: 0x060012B1 RID: 4785 RVA: 0x0002A566 File Offset: 0x00028766
			// (set) Token: 0x060012B2 RID: 4786 RVA: 0x0002A56E File Offset: 0x0002876E
			public string FailedLink { get; set; }

			// Token: 0x170003AC RID: 940
			// (get) Token: 0x060012B3 RID: 4787 RVA: 0x0002A577 File Offset: 0x00028777
			// (set) Token: 0x060012B4 RID: 4788 RVA: 0x0002A57F File Offset: 0x0002877F
			public Exception Exception { get; set; }
		}
	}
}
