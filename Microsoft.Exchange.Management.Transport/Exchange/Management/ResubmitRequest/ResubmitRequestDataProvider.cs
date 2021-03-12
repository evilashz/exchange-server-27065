using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Transport.MessageRepository;

namespace Microsoft.Exchange.Management.ResubmitRequest
{
	// Token: 0x02000084 RID: 132
	public class ResubmitRequestDataProvider : IConfigDataProvider
	{
		// Token: 0x0600048F RID: 1167 RVA: 0x00011908 File Offset: 0x0000FB08
		public ResubmitRequestDataProvider(ServerIdParameter serverId, ResubmitRequestId identity)
		{
			this.serverIdentity = (serverId ?? new ServerIdParameter());
			this.resubmitRequestIdentity = identity;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00011928 File Offset: 0x0000FB28
		private void ResolveServer()
		{
			if (this.serverObject != null)
			{
				return;
			}
			ITopologyConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 66, "ResolveServer", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\ResubmitRequest\\ResubmitRequestDataProvider.cs");
			Server server = null;
			IEnumerable<Server> objects = this.serverIdentity.GetObjects<Server>(null, session);
			IEnumerator<Server> enumerator = objects.GetEnumerator();
			try
			{
				if (!enumerator.MoveNext())
				{
					throw new LocalizedException(Strings.ErrorServerNotFound(this.serverIdentity));
				}
				server = enumerator.Current;
				if (enumerator.MoveNext())
				{
					throw new LocalizedException(Strings.ErrorServerNotUnique(this.serverIdentity));
				}
			}
			finally
			{
				enumerator.Dispose();
			}
			this.serverObject = server;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000119CC File Offset: 0x0000FBCC
		IConfigurable IConfigDataProvider.Read<T>(ObjectId identity)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x000119DC File Offset: 0x0000FBDC
		IConfigurable[] IConfigDataProvider.Find<T>(QueryFilter notUsed1, ObjectId notUsed2, bool notUsed3, SortBy notUsed4)
		{
			this.ResolveServer();
			MessageResubmissionRpcClientImpl messageResubmissionRpcClientImpl = new MessageResubmissionRpcClientImpl(this.serverObject.Name);
			return (from item in messageResubmissionRpcClientImpl.GetResubmitRequest(this.resubmitRequestIdentity)
			select this.GetPresentationObject(item)).ToArray<ResubmitRequest>();
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00011BE4 File Offset: 0x0000FDE4
		IEnumerable<T> IConfigDataProvider.FindPaged<T>(QueryFilter notUsed1, ObjectId notUsed2, bool notUsed3, SortBy notUsed4, int notUsed5)
		{
			this.ResolveServer();
			MessageResubmissionRpcClientImpl client = new MessageResubmissionRpcClientImpl(this.serverObject.Name);
			foreach (ResubmitRequest request in client.GetResubmitRequest(this.resubmitRequestIdentity))
			{
				yield return (T)((object)this.GetPresentationObject(request));
			}
			yield break;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00011C04 File Offset: 0x0000FE04
		void IConfigDataProvider.Save(IConfigurable instance)
		{
			this.ResolveServer();
			ResubmitRequest resubmitRequest = (ResubmitRequest)instance;
			if (resubmitRequest.State != ResubmitRequestState.Running && resubmitRequest.State != ResubmitRequestState.Paused)
			{
				throw new InvalidOperationException(new LocalizedString(Strings.ResubmitRequestStateInvalid));
			}
			MessageResubmissionRpcClientImpl messageResubmissionRpcClientImpl = new MessageResubmissionRpcClientImpl(this.serverObject.Name);
			messageResubmissionRpcClientImpl.SetResubmitRequest((ResubmitRequestId)resubmitRequest.Identity, resubmitRequest.State == ResubmitRequestState.Running);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00011C78 File Offset: 0x0000FE78
		void IConfigDataProvider.Delete(IConfigurable instance)
		{
			this.ResolveServer();
			MessageResubmissionRpcClientImpl messageResubmissionRpcClientImpl = new MessageResubmissionRpcClientImpl(this.serverObject.Name);
			messageResubmissionRpcClientImpl.RemoveResubmitRequest((ResubmitRequestId)instance.Identity);
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00011CAD File Offset: 0x0000FEAD
		string IConfigDataProvider.Source
		{
			get
			{
				return this.serverIdentity.Fqdn;
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00011CBC File Offset: 0x0000FEBC
		private ResubmitRequest GetPresentationObject(ResubmitRequest original)
		{
			return ResubmitRequest.Create(original.ResubmitRequestId.ResubmitRequestRowId, original.Server, original.StartTime.ToLocalTime(), original.Destination, original.DiagnosticInformation, original.EndTime.ToLocalTime(), original.CreationTime.ToLocalTime(), (int)original.State);
		}

		// Token: 0x0400018D RID: 397
		private readonly ServerIdParameter serverIdentity;

		// Token: 0x0400018E RID: 398
		private readonly ResubmitRequestId resubmitRequestIdentity;

		// Token: 0x0400018F RID: 399
		private Server serverObject;
	}
}
