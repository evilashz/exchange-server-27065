using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Management.Migration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.DataAccessLayer;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000058 RID: 88
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationEndpointDataProvider : XsoMailboxDataProviderBase
	{
		// Token: 0x0600042A RID: 1066 RVA: 0x0000F9BC File Offset: 0x0000DBBC
		internal MigrationEndpointDataProvider(IMigrationDataProvider dataProvider)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			this.diagnosticEnabled = false;
			this.dataProvider = dataProvider;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000F9DD File Offset: 0x0000DBDD
		private MigrationEndpointDataProvider(MigrationDataProvider dataProvider) : base(dataProvider.MailboxSession)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			this.dataProvider = dataProvider;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000FA00 File Offset: 0x0000DC00
		public static MigrationEndpointDataProvider CreateDataProvider(string action, IRecipientSession recipientSession, ADUser partitionMailbox)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(action, "action");
			MigrationUtil.ThrowOnNullArgument(recipientSession, "recipientSession");
			MigrationEndpointDataProvider result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				MigrationDataProvider disposable = MigrationDataProvider.CreateProviderForMigrationMailbox(action, recipientSession, partitionMailbox);
				disposeGuard.Add<MigrationDataProvider>(disposable);
				MigrationEndpointDataProvider migrationEndpointDataProvider = new MigrationEndpointDataProvider(disposable);
				disposeGuard.Success();
				result = migrationEndpointDataProvider;
			}
			return result;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000FA70 File Offset: 0x0000DC70
		public static QueryFilter GetFilterFromEndpointType(MigrationType type)
		{
			return new ComparisonFilter(ComparisonOperator.Equal, MigrationEndpointMessageSchema.MigrationEndpointType, type);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000FA84 File Offset: 0x0000DC84
		public static QueryFilter GetFilterFromConnectionSettings(ExchangeConnectionSettings connectionSettings)
		{
			List<QueryFilter> list = new List<QueryFilter>();
			MigrationType type = connectionSettings.Type;
			if (type != MigrationType.ExchangeOutlookAnywhere)
			{
				if (type != MigrationType.ExchangeRemoteMove)
				{
					if (type != MigrationType.PublicFolder)
					{
						throw new NotSupportedException(string.Format("The connection settings of type '{0}' is not for a well-known Exchange endpoint.", connectionSettings.Type));
					}
					list.Add(MigrationEndpointDataProvider.GetFilterFromEndpointType(MigrationType.PublicFolder));
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, MigrationEndpointMessageSchema.RemoteHostName, connectionSettings.IncomingRPCProxyServer));
				}
				else
				{
					list.Add(MigrationEndpointDataProvider.GetFilterFromEndpointType(MigrationType.ExchangeRemoteMove));
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, MigrationEndpointMessageSchema.RemoteHostName, connectionSettings.IncomingRPCProxyServer));
				}
			}
			else
			{
				list.Add(MigrationEndpointDataProvider.GetFilterFromEndpointType(MigrationType.ExchangeOutlookAnywhere));
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, MigrationEndpointMessageSchema.ExchangeServer, connectionSettings.IncomingExchangeServer));
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, MigrationEndpointMessageSchema.RemoteHostName, connectionSettings.IncomingRPCProxyServer));
			}
			return new AndFilter(list.ToArray());
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000FB68 File Offset: 0x0000DD68
		public void EnableDiagnostics(string argument)
		{
			this.diagnosticEnabled = true;
			this.diagnosticArgument = new MigrationDiagnosticArgument(argument);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000FB80 File Offset: 0x0000DD80
		internal void CreateEndpoint(MigrationEndpoint connector)
		{
			MigrationEndpointBase migrationEndpointBase = MigrationEndpointBase.CreateFrom(connector);
			MigrationEndpointBase.Create(this.dataProvider, migrationEndpointBase);
			connector.Identity = migrationEndpointBase.Identity;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000FE58 File Offset: 0x0000E058
		protected override IEnumerable<T> InternalFindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			MigrationEndpointId endpointRoot = rootId as MigrationEndpointId;
			IEnumerable<MigrationEndpointBase> endpoints;
			if (endpointRoot != null)
			{
				endpoints = MigrationEndpointBase.Get(endpointRoot, this.dataProvider, true);
			}
			else
			{
				endpoints = MigrationEndpointBase.Get(filter, this.dataProvider, true);
			}
			foreach (MigrationEndpointBase item in endpoints)
			{
				if (typeof(MigrationEndpoint).IsAssignableFrom(typeof(T)))
				{
					MigrationEndpoint endpoint = item.ToMigrationEndpoint();
					if (this.diagnosticEnabled)
					{
						XElement diagnosticInfo = item.GetDiagnosticInfo(this.dataProvider, this.diagnosticArgument);
						if (diagnosticInfo != null)
						{
							endpoint.DiagnosticInfo = diagnosticInfo.ToString();
						}
					}
					yield return (T)((object)endpoint);
				}
				else
				{
					yield return (T)((object)item);
				}
			}
			yield break;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000FE84 File Offset: 0x0000E084
		protected override void InternalSave(ConfigurableObject instance)
		{
			MigrationEndpoint migrationEndpoint = instance as MigrationEndpoint;
			if (migrationEndpoint == null)
			{
				throw new ArgumentException("Instance is not from an expected class.", "instance");
			}
			migrationEndpoint.LastModifiedTime = (DateTime)ExDateTime.UtcNow;
			switch (instance.ObjectState)
			{
			case ObjectState.New:
				this.CreateEndpoint(migrationEndpoint);
				return;
			case ObjectState.Unchanged:
			case ObjectState.Changed:
				MigrationEndpointBase.UpdateEndpoint(migrationEndpoint, this.dataProvider);
				return;
			case ObjectState.Deleted:
				return;
			default:
				throw new NotImplementedException("Support for action " + instance.ObjectState + " is not yet implemented");
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000FF10 File Offset: 0x0000E110
		protected override void InternalDelete(ConfigurableObject instance)
		{
			MigrationEndpoint migrationEndpoint = instance as MigrationEndpoint;
			if (migrationEndpoint == null)
			{
				throw new ArgumentException("Instance is not from an expected class.", "instance");
			}
			switch (instance.ObjectState)
			{
			case ObjectState.Unchanged:
			case ObjectState.Deleted:
				MigrationEndpointBase.Delete(migrationEndpoint.Identity, this.dataProvider);
				return;
			}
			throw new NotImplementedException("Support for action " + instance.ObjectState + " is not yet implemented");
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000FF85 File Offset: 0x0000E185
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MigrationEndpointDataProvider>(this);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000FF90 File Offset: 0x0000E190
		protected override void InternalDispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this.dataProvider != null)
					{
						this.dataProvider.Dispose();
					}
					this.dataProvider = null;
				}
			}
			finally
			{
				base.InternalDispose(disposing);
			}
		}

		// Token: 0x04000153 RID: 339
		private IMigrationDataProvider dataProvider;

		// Token: 0x04000154 RID: 340
		private bool diagnosticEnabled;

		// Token: 0x04000155 RID: 341
		private MigrationDiagnosticArgument diagnosticArgument;
	}
}
