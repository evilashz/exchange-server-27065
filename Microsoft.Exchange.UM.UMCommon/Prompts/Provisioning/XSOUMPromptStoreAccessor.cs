using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.Prompts.Provisioning
{
	// Token: 0x0200008C RID: 140
	internal class XSOUMPromptStoreAccessor : DisposableBase, IUMPromptStorage, IDisposeTrackable, IDisposable
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00012718 File Offset: 0x00010918
		private string MappingTableName
		{
			get
			{
				return XsoUtil.CombineConfigurationNames("Um.CustomPromptMappingTable", this.configurationObject.ToString("N"));
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00012734 File Offset: 0x00010934
		private string ConfigurationBaseName
		{
			get
			{
				return XsoUtil.CombineConfigurationNames("Um.CustomPrompts.", this.configurationObject.ToString().Replace("-", string.Empty));
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00012760 File Offset: 0x00010960
		private UserConfiguration MappingTable
		{
			get
			{
				if (this.lazyMappingTable == null)
				{
					this.lazyMappingTable = this.GetConfigurationObject(this.MappingTableName, UserConfigurationTypes.Dictionary, true);
				}
				return this.lazyMappingTable;
			}
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x000127BC File Offset: 0x000109BC
		public XSOUMPromptStoreAccessor(ExchangePrincipal user, Guid configurationObject)
		{
			XSOUMPromptStoreAccessor <>4__this = this;
			this.mailboxPrincipal = user;
			this.disposeMailboxSession = true;
			this.tracer = new DiagnosticHelper(this, ExTraceGlobals.XsoTracer);
			this.ExecuteXSOOperation(delegate
			{
				<>4__this.Initialize(MailboxSessionEstablisher.OpenAsAdmin(<>4__this.mailboxPrincipal, CultureInfo.InvariantCulture, "Client=UM;Action=UMPublishingMailbox-Manage-CustomPrompts"), configurationObject);
			});
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001281B File Offset: 0x00010A1B
		public XSOUMPromptStoreAccessor(MailboxSession session, Guid configurationObject)
		{
			this.tracer = new DiagnosticHelper(this, ExTraceGlobals.XsoTracer);
			this.Initialize(session, configurationObject);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001283C File Offset: 0x00010A3C
		public void DeleteAllPrompts()
		{
			this.tracer.Trace("XSOUMPromptsStorage : DeleteAllPrompts", new object[0]);
			ICollection<UserConfiguration> publishedContent = this.GetPublishedContent();
			List<string> list = new List<string>(publishedContent.Count);
			foreach (UserConfiguration userConfiguration in publishedContent)
			{
				using (userConfiguration)
				{
					list.Add(userConfiguration.ConfigurationName);
				}
			}
			OperationResult operationResult = this.lazyPublishingSessionMailbox.UserConfigurationManager.DeleteMailboxConfigurations(list.ToArray());
			if (OperationResult.Succeeded != operationResult)
			{
				throw new DeleteContentException(operationResult.ToString());
			}
			this.lazyPublishingSessionMailbox.UserConfigurationManager.DeleteMailboxConfigurations(new string[]
			{
				this.MappingTableName
			});
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x000129B4 File Offset: 0x00010BB4
		public void DeletePrompts(string[] prompts)
		{
			this.tracer.Trace("XSOUMPromptsStorage : DeletePrompts", new object[0]);
			this.ExecuteXSOOperation(delegate
			{
				ValidateArgument.NotNull(prompts, "Prompts");
				List<string> list = new List<string>(prompts.Length);
				foreach (string fileName in prompts)
				{
					string text = this.ConfigurationNameFromFileName(fileName, false);
					if (!string.IsNullOrEmpty(text))
					{
						list.Add(text);
					}
				}
				this.lazyPublishingSessionMailbox.UserConfigurationManager.DeleteMailboxConfigurations(list.ToArray());
			});
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000129FD File Offset: 0x00010BFD
		public string[] GetPromptNames()
		{
			return this.GetPromptNames(TimeSpan.Zero);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00012AFC File Offset: 0x00010CFC
		public string[] GetPromptNames(TimeSpan timeSinceLastModified)
		{
			this.tracer.Trace("XSOUMPromptsStorage : GetPromptNames, for Guid {0}", new object[]
			{
				this.configurationObject
			});
			List<string> prompts = new List<string>();
			this.ExecuteXSOOperation(delegate
			{
				ICollection<UserConfiguration> publishedContent = this.GetPublishedContent();
				List<string> list = new List<string>();
				foreach (UserConfiguration userConfiguration in publishedContent)
				{
					using (userConfiguration)
					{
						string text = this.FileNameFromConfigurationName(userConfiguration.ConfigurationName);
						if (!string.IsNullOrEmpty(text))
						{
							if (ExDateTime.UtcNow.Subtract(userConfiguration.LastModifiedTime) >= timeSinceLastModified)
							{
								prompts.Add(text);
							}
						}
						else
						{
							list.Add(userConfiguration.ConfigurationName);
						}
					}
				}
				this.lazyPublishingSessionMailbox.UserConfigurationManager.DeleteMailboxConfigurations(list.ToArray());
			});
			return prompts.ToArray();
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00012C54 File Offset: 0x00010E54
		public void CreatePrompt(string promptName, string audioBytes)
		{
			this.ExecuteXSOOperation(delegate
			{
				ValidateArgument.NotNullOrEmpty(promptName, "promptName");
				ExAssert.RetailAssert(audioBytes != null && audioBytes.Length > 0, "AudioData passed cannot be null or empty");
				this.tracer.Trace("XSOUMPromptsStorage : CreatePrompt, promptName {0}", new object[]
				{
					promptName
				});
				string configName = this.ConfigurationNameFromFileName(promptName, true);
				using (UserConfiguration userConfiguration = this.GetConfigurationObject(configName, UserConfigurationTypes.Stream, true))
				{
					using (Stream stream = userConfiguration.GetStream())
					{
						stream.SetLength(0L);
						CommonUtil.CopyBase64StringToSteam(audioBytes, stream);
						userConfiguration.Save();
					}
				}
			});
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00012D34 File Offset: 0x00010F34
		public string GetPrompt(string promptName)
		{
			ValidateArgument.NotNullOrEmpty(promptName, "promptName");
			this.tracer.Trace("XSOUMPromptsStorage : GetPrompt, promptName {0}", new object[]
			{
				promptName
			});
			string promptBytes = null;
			this.ExecuteXSOOperation(delegate
			{
				string text = this.ConfigurationNameFromFileName(promptName, false);
				if (string.IsNullOrEmpty(text))
				{
					throw new SourceFileNotFoundException(promptName);
				}
				using (UserConfiguration userConfiguration = this.GetConfigurationObject(text, UserConfigurationTypes.Stream, false))
				{
					if (userConfiguration == null)
					{
						throw new SourceFileNotFoundException(promptName);
					}
					using (Stream stream = userConfiguration.GetStream())
					{
						promptBytes = CommonUtil.GetBase64StringFromStream(stream);
					}
				}
			});
			return promptBytes;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00012DA8 File Offset: 0x00010FA8
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.tracer.Trace("XSOUMPromptsStorage : InternalDispose", new object[0]);
				if (this.lazyMappingTable != null)
				{
					this.lazyMappingTable.Dispose();
				}
				if (this.lazyPublishingSessionMailbox != null && this.disposeMailboxSession)
				{
					this.lazyPublishingSessionMailbox.Dispose();
				}
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00012DFC File Offset: 0x00010FFC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<XSOUMPromptStoreAccessor>(this);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00012E04 File Offset: 0x00011004
		private void Initialize(MailboxSession session, Guid configurationObject)
		{
			ExAssert.RetailAssert(configurationObject != Guid.Empty, "ConfigurationObject Guid cannot be empty");
			ExAssert.RetailAssert(session != null, "MailboxSession cannot be null");
			this.configurationObject = configurationObject;
			this.lazyPublishingSessionMailbox = session;
			this.tracer.Trace("XSOUMPromptsStorage for configObject {0}, called from WebServices : {1}", new object[]
			{
				configurationObject,
				!this.disposeMailboxSession
			});
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00012E78 File Offset: 0x00011078
		private string FileNameFromConfigurationName(string configName)
		{
			string result = null;
			IDictionary dictionary = this.MappingTable.GetDictionary();
			if (dictionary.Contains(configName))
			{
				result = (dictionary[configName] as string);
			}
			return result;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00012EAC File Offset: 0x000110AC
		private string ConfigurationNameFromFileName(string fileName, bool create)
		{
			string text = null;
			fileName = fileName.ToLowerInvariant();
			IDictionary dictionary = this.MappingTable.GetDictionary();
			if (dictionary.Contains(fileName))
			{
				text = (dictionary[fileName] as string);
			}
			if (create && string.IsNullOrEmpty(text))
			{
				string c = Guid.NewGuid().ToString().Replace("-", string.Empty);
				text = XsoUtil.CombineConfigurationNames(this.ConfigurationBaseName, c);
				dictionary[fileName] = text;
				dictionary[text] = fileName;
				this.MappingTable.Save();
				this.lazyMappingTable.Dispose();
				this.lazyMappingTable = null;
			}
			return text;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00012F50 File Offset: 0x00011150
		private ICollection<UserConfiguration> GetPublishedContent()
		{
			return this.lazyPublishingSessionMailbox.UserConfigurationManager.FindMailboxConfigurations(this.ConfigurationBaseName, UserConfigurationSearchFlags.Prefix);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00012F78 File Offset: 0x00011178
		private UserConfiguration GetConfigurationObject(string configName, UserConfigurationTypes configType, bool create)
		{
			UserConfiguration userConfiguration = null;
			try
			{
				userConfiguration = this.lazyPublishingSessionMailbox.UserConfigurationManager.GetMailboxConfiguration(configName, configType);
			}
			catch (ObjectNotFoundException)
			{
			}
			catch (CorruptDataException)
			{
				this.lazyPublishingSessionMailbox.UserConfigurationManager.DeleteMailboxConfigurations(new string[]
				{
					configName
				});
			}
			catch (InvalidOperationException)
			{
				this.lazyPublishingSessionMailbox.UserConfigurationManager.DeleteMailboxConfigurations(new string[]
				{
					configName
				});
			}
			if (userConfiguration == null && create)
			{
				this.tracer.Trace("Creating config object {0}", new object[]
				{
					configName
				});
				userConfiguration = this.lazyPublishingSessionMailbox.UserConfigurationManager.CreateMailboxConfiguration(configName, configType);
			}
			return userConfiguration;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00013040 File Offset: 0x00011240
		private void ExecuteXSOOperation(Action function)
		{
			try
			{
				function();
			}
			catch (Exception ex)
			{
				this.tracer.Trace("XSOUMPromptsStorage ExecuteXSOOperation, exception  = {0}", new object[]
				{
					ex
				});
				if (this.mailboxPrincipal != null)
				{
					XsoUtil.LogMailboxConnectionFailureException(ex, this.mailboxPrincipal);
				}
				if (ex is StoragePermanentException || ex is StorageTransientException)
				{
					throw new PublishingPointException(ex.Message, ex);
				}
				throw;
			}
		}

		// Token: 0x04000312 RID: 786
		private readonly bool disposeMailboxSession;

		// Token: 0x04000313 RID: 787
		private Guid configurationObject;

		// Token: 0x04000314 RID: 788
		private ExchangePrincipal mailboxPrincipal;

		// Token: 0x04000315 RID: 789
		private MailboxSession lazyPublishingSessionMailbox;

		// Token: 0x04000316 RID: 790
		private UserConfiguration lazyMappingTable;

		// Token: 0x04000317 RID: 791
		private DiagnosticHelper tracer;
	}
}
