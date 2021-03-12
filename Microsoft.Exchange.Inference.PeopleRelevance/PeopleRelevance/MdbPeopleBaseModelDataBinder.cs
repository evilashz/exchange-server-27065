using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Inference;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Inference.Mdb;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x02000008 RID: 8
	internal abstract class MdbPeopleBaseModelDataBinder<TModelItem> : IInferenceModelDataBinder<TModelItem> where TModelItem : InferenceBaseModelItem, new()
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002812 File Offset: 0x00000A12
		protected MdbPeopleBaseModelDataBinder(string modelFAIName, MailboxSession session)
		{
			Util.ThrowOnNullArgument(session, "session");
			this.session = session;
			this.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("MdbPeopleBaseModelDataBinder", ExTraceGlobals.MdbInferenceModelDataBinderTracer, (long)this.GetHashCode());
			this.modelFAIName = modelFAIName;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000284F File Offset: 0x00000A4F
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002856 File Offset: 0x00000A56
		public static IList<Type> KnownTypesForSerialization
		{
			get
			{
				return MdbPeopleBaseModelDataBinder<TModelItem>.knownTypesForSerialization;
			}
			set
			{
				MdbPeopleBaseModelDataBinder<TModelItem>.knownTypesForSerialization = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000050 RID: 80
		protected abstract Version MinimumSupportedVersion { get; }

		// Token: 0x06000051 RID: 81 RVA: 0x00002860 File Offset: 0x00000A60
		public virtual TModelItem GetModelData()
		{
			TModelItem tmodelItem = default(TModelItem);
			using (UserConfiguration userConfiguration = this.GetUserConfiguration(this.modelFAIName, true))
			{
				using (Stream modelStreamFromUserConfig = this.GetModelStreamFromUserConfig(userConfiguration))
				{
					if (modelStreamFromUserConfig.Length == 0L)
					{
						tmodelItem = Activator.CreateInstance<TModelItem>();
					}
					else
					{
						DataContractSerializer serializer = new DataContractSerializer(typeof(TModelItem), MdbPeopleBaseModelDataBinder<TModelItem>.KnownTypesForSerialization);
						try
						{
							tmodelItem = this.ReadModelData(serializer, modelStreamFromUserConfig);
							if (tmodelItem == null)
							{
								this.diagnosticsSession.TraceError("Deserializing the stream returned a type other than inference model item", new object[0]);
								tmodelItem = Activator.CreateInstance<TModelItem>();
							}
							else if (tmodelItem.Version < this.MinimumSupportedVersion)
							{
								this.diagnosticsSession.TraceDebug<Version>("Returning a new InferenceModelItem since version {0} in the existing model is not supported.", tmodelItem.Version);
								tmodelItem = Activator.CreateInstance<TModelItem>();
							}
						}
						catch (SerializationException arg)
						{
							this.diagnosticsSession.TraceError<SerializationException>("Received serialization exception - {0}", arg);
							tmodelItem = Activator.CreateInstance<TModelItem>();
							using (this.ResetModel(true))
							{
							}
						}
						catch (ArgumentException arg2)
						{
							this.diagnosticsSession.TraceError<ArgumentException>("Received argument exception - {0}", arg2);
							tmodelItem = Activator.CreateInstance<TModelItem>();
							using (this.ResetModel(true))
							{
							}
						}
					}
				}
			}
			return tmodelItem;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A38 File Offset: 0x00000C38
		public virtual long SaveModelData(TModelItem modelData)
		{
			long result = -1L;
			using (UserConfiguration userConfiguration = this.GetUserConfiguration(this.modelFAIName, true))
			{
				using (Stream modelStreamFromUserConfig = this.GetModelStreamFromUserConfig(userConfiguration))
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(TModelItem), MdbPeopleBaseModelDataBinder<TModelItem>.KnownTypesForSerialization);
					try
					{
						this.WriteModelData(serializer, modelStreamFromUserConfig, modelData);
						userConfiguration.Save();
						result = modelStreamFromUserConfig.Length;
					}
					catch (SerializationException ex)
					{
						this.diagnosticsSession.TraceError<SerializationException>("Received serialization exception - {0}", ex);
						this.diagnosticsSession.SendInformationalWatsonReport(ex, "Model cannot be serialized");
					}
					catch (MessageSubmissionExceededException ex2)
					{
						this.diagnosticsSession.TraceError<MessageSubmissionExceededException>("Received MessageSubmissionExceeded exception - {0}", ex2);
						this.diagnosticsSession.SendInformationalWatsonReport(ex2, "Model cannot be saved to store (SaveModelData)");
					}
				}
			}
			return result;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002B28 File Offset: 0x00000D28
		internal UserConfiguration ResetModel(bool deleteOld)
		{
			return XsoUtil.ResetModel(this.modelFAIName, this.GetUserConfigurationType(), this.session, deleteOld, this.diagnosticsSession);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002B48 File Offset: 0x00000D48
		internal UserConfiguration GetUserConfiguration(string userConfigurationName, bool createIfMissing)
		{
			UserConfiguration userConfiguration = null;
			StoreId defaultFolderId = this.session.GetDefaultFolderId(DefaultFolderType.Inbox);
			bool deleteOld = false;
			Exception arg = null;
			try
			{
				userConfiguration = this.session.UserConfigurationManager.GetFolderConfiguration(userConfigurationName, this.GetUserConfigurationType(), defaultFolderId);
			}
			catch (ObjectNotFoundException ex)
			{
				arg = ex;
			}
			catch (CorruptDataException ex2)
			{
				arg = ex2;
				deleteOld = true;
			}
			if (userConfiguration == null)
			{
				this.diagnosticsSession.TraceDebug<string, Exception>("FAI message '{0}' is missing or corrupt. Exception: {1}", userConfigurationName, arg);
				if (createIfMissing)
				{
					userConfiguration = this.ResetModel(deleteOld);
				}
			}
			return userConfiguration;
		}

		// Token: 0x06000055 RID: 85
		internal abstract Stream GetModelStreamFromUserConfig(UserConfiguration config);

		// Token: 0x06000056 RID: 86
		protected abstract UserConfigurationTypes GetUserConfigurationType();

		// Token: 0x06000057 RID: 87
		protected abstract void WriteModelData(DataContractSerializer serializer, Stream stream, TModelItem modelData);

		// Token: 0x06000058 RID: 88
		protected abstract TModelItem ReadModelData(DataContractSerializer serializer, Stream stream);

		// Token: 0x0400001F RID: 31
		private const string ComponentName = "MdbPeopleBaseModelDataBinder";

		// Token: 0x04000020 RID: 32
		private static IList<Type> knownTypesForSerialization = new List<Type>
		{
			typeof(MdbRecipient),
			typeof(MdbRecipientIdentity),
			typeof(MdbCompositeItemIdentity)
		};

		// Token: 0x04000021 RID: 33
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x04000022 RID: 34
		private readonly string modelFAIName;

		// Token: 0x04000023 RID: 35
		private MailboxSession session;
	}
}
