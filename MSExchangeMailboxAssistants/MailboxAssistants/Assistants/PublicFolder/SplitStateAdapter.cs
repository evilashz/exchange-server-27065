using System;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.PublicFolder;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x02000186 RID: 390
	internal class SplitStateAdapter : ISplitStateAdapter
	{
		// Token: 0x06000F82 RID: 3970 RVA: 0x0005BF4D File Offset: 0x0005A14D
		internal SplitStateAdapter(IPublicFolderSession publicFolderSession, IXSOFactory xsoFactory, IPublicFolderMailboxLoggerBase logger)
		{
			this.publicFolderSession = publicFolderSession;
			this.xsoFactory = xsoFactory;
			this.logger = logger;
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0005BF6C File Offset: 0x0005A16C
		public IPublicFolderSplitState ReadFromStore(out Exception error)
		{
			IPublicFolderSplitState publicFolderSplitState = null;
			error = null;
			try
			{
				using (Folder folder = this.xsoFactory.BindToFolder(this.publicFolderSession, this.publicFolderSession.GetTombstonesRootFolderId()) as Folder)
				{
					using (UserConfiguration configuration = UserConfiguration.GetConfiguration(folder, new UserConfigurationName("PublicFolderSplitState", ConfigurationNameKind.Name), UserConfigurationTypes.Stream))
					{
						using (Stream stream = configuration.GetStream())
						{
							if (stream.Length == 0L)
							{
								publicFolderSplitState = new PublicFolderSplitState();
							}
							else
							{
								try
								{
									publicFolderSplitState = (SplitStateAdapter.Serializer.ReadObject(stream) as PublicFolderSplitState);
									if (publicFolderSplitState == null || publicFolderSplitState.VersionNumber != SplitStateAdapter.CurrentVersion)
									{
										publicFolderSplitState = new PublicFolderSplitState();
										this.logger.LogEvent(LogEventType.Verbose, string.Format("SplitStateAdapter::{0}::ReadFromStore - Unrecognized split state type for {1}", this.GetHashCode(), this.publicFolderSession.DisplayAddress));
									}
								}
								catch (SerializationException arg)
								{
									publicFolderSplitState = new PublicFolderSplitState();
									this.logger.LogEvent(LogEventType.Verbose, string.Format("SplitStateAdapter::{0}::ReadFromStore - Received serialization exception for {1} - {2}", this.GetHashCode(), this.publicFolderSession.DisplayAddress, arg));
								}
							}
						}
					}
				}
			}
			catch (StorageTransientException ex)
			{
				error = ex;
			}
			catch (StoragePermanentException ex2)
			{
				error = ex2;
			}
			if (error != null)
			{
				this.logger.LogEvent(LogEventType.Verbose, string.Format("SplitStateAdapter::{0}::ReadFromStore - Error reading the split state for {1} - {2}", this.GetHashCode(), this.publicFolderSession.DisplayAddress, error));
			}
			return publicFolderSplitState;
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0005C160 File Offset: 0x0005A360
		public Exception SaveToStore(IPublicFolderSplitState splitState)
		{
			Exception ex = null;
			try
			{
				using (Folder folder = this.xsoFactory.BindToFolder(this.publicFolderSession, this.publicFolderSession.GetTombstonesRootFolderId()) as Folder)
				{
					using (UserConfiguration configuration = UserConfiguration.GetConfiguration(folder, new UserConfigurationName("PublicFolderSplitState", ConfigurationNameKind.Name), UserConfigurationTypes.Stream))
					{
						using (Stream stream = configuration.GetStream())
						{
							SplitStateAdapter.Serializer.WriteObject(stream, splitState);
							stream.SetLength(stream.Position);
							configuration.Save();
						}
					}
				}
			}
			catch (SerializationException ex2)
			{
				ex = ex2;
			}
			catch (StorageTransientException ex3)
			{
				ex = ex3;
			}
			catch (StoragePermanentException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				this.logger.LogEvent(LogEventType.Verbose, string.Format("SplitStateAdapter::{0}::ReadFromStore - Error writing the split state for {1} - {2}", this.GetHashCode(), this.publicFolderSession.DisplayAddress, ex));
			}
			return ex;
		}

		// Token: 0x040009DB RID: 2523
		internal const string PublicFolderSplitStateName = "PublicFolderSplitState";

		// Token: 0x040009DC RID: 2524
		internal static Version CurrentVersion = new Version(1, 1);

		// Token: 0x040009DD RID: 2525
		internal static NetDataContractSerializer Serializer = new NetDataContractSerializer();

		// Token: 0x040009DE RID: 2526
		private readonly IXSOFactory xsoFactory;

		// Token: 0x040009DF RID: 2527
		private readonly IPublicFolderSession publicFolderSession;

		// Token: 0x040009E0 RID: 2528
		private readonly IPublicFolderMailboxLoggerBase logger;
	}
}
