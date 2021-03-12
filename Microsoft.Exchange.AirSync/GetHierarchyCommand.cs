using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000B5 RID: 181
	internal sealed class GetHierarchyCommand : Command
	{
		// Token: 0x060009B9 RID: 2489 RVA: 0x00039352 File Offset: 0x00037552
		internal GetHierarchyCommand()
		{
			base.PerfCounter = AirSyncCounters.NumberOfGetHierarchy;
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x00039365 File Offset: 0x00037565
		internal override int MaxVersion
		{
			get
			{
				return 121;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x00039369 File Offset: 0x00037569
		protected sealed override string RootNodeName
		{
			get
			{
				return "Invalid";
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00039370 File Offset: 0x00037570
		internal override Command.ExecutionState ExecuteCommand()
		{
			object[][] array = null;
			try
			{
				using (Folder folder = Folder.Bind(base.MailboxSession, DefaultFolderType.Root))
				{
					using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.DeepTraversal, AirSyncUtility.XsoFilters.GetHierarchyFilter, null, GetHierarchyCommand.folderPropertyIds))
					{
						array = queryResult.GetRows(10000);
						if (queryResult.EstimatedRowCount > 10000)
						{
							base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "TooManyFolders");
							throw new AirSyncPermanentException(HttpStatusCode.InternalServerError, StatusCode.TooManyFolders, null, false);
						}
						if (array.Length <= 10000)
						{
							AirSyncDiagnostics.TraceDebug<int, int>(ExTraceGlobals.XsoTracer, this, "Query backend for more folders: XSO MaxRow = {0}, Sync MaxNumOfFolders = {1}", 10000, GlobalSettings.MaxNumOfFolders);
							List<object[]> list = new List<object[]>();
							for (int i = 0; i < array.Length; i++)
							{
								list.Add(array[i]);
							}
							while (list.Count < 10000 && array.Length > 0)
							{
								array = queryResult.GetRows(10000);
								int num = 0;
								while (num < array.Length && list.Count < 10000)
								{
									list.Add(array[num]);
									num++;
								}
							}
							array = new object[list.Count][];
							list.CopyTo(array);
						}
					}
				}
				Array.Sort<object[]>(array, new Comparison<object[]>(GetHierarchyCommand.CompareFolderDepth));
				if (array == null || array.Length == 0)
				{
					base.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "NoFoldersFound");
					throw new AirSyncPermanentException(HttpStatusCode.InternalServerError, StatusCode.NoFoldersFound, null, false);
				}
				this.RemoveIsolatedFolders(ref array);
				this.LoadSyncState();
				base.XmlResponse = this.CreateXmlResponse(array);
				base.ProtocolLogger.IncrementValueBy("F", PerFolderProtocolLoggerData.ServerAdds, array.Length);
				this.folderIdMappingSyncState.Commit();
			}
			catch (Exception)
			{
				base.ProtocolLogger.IncrementValue(ProtocolLoggerData.NumErrors);
				throw;
			}
			finally
			{
				if (this.folderIdMappingSyncState != null)
				{
					this.folderIdMappingSyncState.Dispose();
				}
			}
			return Command.ExecutionState.Complete;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x000395A0 File Offset: 0x000377A0
		protected override bool HandleQuarantinedState()
		{
			return true;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x000395A4 File Offset: 0x000377A4
		private static int CompareFolderDepth(object[] x, object[] y)
		{
			int num = (int)x[3];
			int value = (int)y[3];
			return num.CompareTo(value);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x000395CC File Offset: 0x000377CC
		private static string GetOrAddSyncFolderId(SyncState folderIdMappingSyncState, StoreObjectId mailboxFolderId)
		{
			FolderIdMapping folderIdMapping = (FolderIdMapping)folderIdMappingSyncState[CustomStateDatumType.IdMapping];
			FolderTree folderTree = (FolderTree)folderIdMappingSyncState[CustomStateDatumType.FullFolderTree];
			MailboxSyncItemId mailboxSyncItemId = MailboxSyncItemId.CreateForNewItem(mailboxFolderId);
			string text = folderIdMapping[mailboxSyncItemId];
			if (text == null)
			{
				text = folderIdMapping.Add(mailboxSyncItemId);
				folderTree.AddFolder(mailboxSyncItemId);
			}
			return text;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00039620 File Offset: 0x00037820
		private XmlDocument CreateXmlResponse(object[][] folders)
		{
			XmlDocument xmlDocument = new SafeXmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("Folders", "FolderHierarchy:");
			xmlDocument.AppendChild(xmlElement);
			for (int i = 0; i < folders.Length; i++)
			{
				this.WriteFolderXml(xmlDocument, xmlElement, folders[i]);
			}
			return xmlDocument;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00039668 File Offset: 0x00037868
		private void LoadSyncState()
		{
			this.folderIdMappingSyncState = base.SyncStateStorage.GetCustomSyncState(new FolderIdMappingSyncStateInfo(), new PropertyDefinition[0]);
			if (this.folderIdMappingSyncState == null || this.folderIdMappingSyncState[CustomStateDatumType.IdMapping] == null)
			{
				CustomSyncState customSyncState = base.SyncStateStorage.GetCustomSyncState(new GlobalSyncStateInfo(), new PropertyDefinition[0]);
				if (customSyncState == null)
				{
					base.SyncStateStorage.DeleteAllSyncStates();
				}
				else
				{
					customSyncState.Dispose();
					using (AutdStatusData autdStatusData = AutdStatusData.Load(base.SyncStateStorage, true, false))
					{
						if (autdStatusData != null)
						{
							base.SyncStateStorage.DeleteAllSyncStates();
						}
					}
				}
				this.folderIdMappingSyncState = base.SyncStateStorage.CreateCustomSyncState(new FolderIdMappingSyncStateInfo());
				this.folderIdMappingSyncState[CustomStateDatumType.IdMapping] = new FolderIdMapping();
				this.folderIdMappingSyncState[CustomStateDatumType.FullFolderTree] = new FolderTree();
				this.folderIdMappingSyncState[CustomStateDatumType.RecoveryFullFolderTree] = this.folderIdMappingSyncState[CustomStateDatumType.FullFolderTree];
			}
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00039778 File Offset: 0x00037978
		private void RemoveIsolatedFolders(ref object[][] folders)
		{
			Dictionary<StoreObjectId, int> dictionary = new Dictionary<StoreObjectId, int>();
			int[] array = new int[folders.Length];
			int num = 0;
			List<int> list = new List<int>();
			for (int i = 0; i < folders.Length; i++)
			{
				StoreObjectId objectId = (folders[i][0] as VersionedId).ObjectId;
				dictionary.Add(objectId, i);
			}
			for (int j = 0; j < folders.Length; j++)
			{
				int num2 = j;
				StoreObjectId storeObjectId = folders[num2][1] as StoreObjectId;
				if (array[num2] != 1 && array[num2] != 2)
				{
					list.Clear();
					for (;;)
					{
						list.Add(num2);
						if (storeObjectId.Equals(base.MailboxSession.GetDefaultFolderId(DefaultFolderType.Root)) || (dictionary.ContainsKey(storeObjectId) && array[dictionary[storeObjectId]] == 2))
						{
							break;
						}
						if (!dictionary.ContainsKey(storeObjectId) || array[dictionary[storeObjectId]] == 1)
						{
							goto IL_ED;
						}
						num2 = dictionary[storeObjectId];
						storeObjectId = (folders[num2][1] as StoreObjectId);
					}
					for (int k = 0; k < list.Count; k++)
					{
						array[list[k]] = 2;
					}
					goto IL_176;
					IL_ED:
					for (int l = 0; l < list.Count; l++)
					{
						array[list[l]] = 1;
						string arg = folders[list[l]][2] as string;
						StoreObjectId objectId2 = (folders[list[l]][0] as VersionedId).ObjectId;
						AirSyncDiagnostics.TraceDebug<string, StoreObjectId>(ExTraceGlobals.AlgorithmTracer, this, "Isolated folder '{0}' with Id '{1}' has been identified and will be removed.", arg, objectId2);
					}
					num += list.Count;
				}
				IL_176:;
			}
			if (num > 0)
			{
				int num3 = 0;
				object[][] array2 = new object[folders.Length - num][];
				for (int m = 0; m < array.Length; m++)
				{
					if (array[m] == 2)
					{
						array2[num3++] = folders[m];
					}
				}
				AirSyncDiagnostics.TraceDebug<int>(ExTraceGlobals.AlgorithmTracer, this, "{0} Isolated folders have been identified and removed from folder table", num);
				folders = array2;
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00039960 File Offset: 0x00037B60
		private void WriteFolderXml(XmlDocument xmlResponse, XmlElement foldersNode, object[] folderProperties)
		{
			StoreObjectId objectId = ((VersionedId)folderProperties[0]).ObjectId;
			StoreObjectId storeObjectId = (StoreObjectId)folderProperties[1];
			string orAddSyncFolderId = GetHierarchyCommand.GetOrAddSyncFolderId(this.folderIdMappingSyncState, objectId);
			string innerText = storeObjectId.Equals(base.MailboxSession.GetDefaultFolderId(DefaultFolderType.Root)) ? "0" : GetHierarchyCommand.GetOrAddSyncFolderId(this.folderIdMappingSyncState, storeObjectId);
			string innerText2 = folderProperties[2] as string;
			string airSyncFolderType = AirSyncUtility.GetAirSyncFolderType(base.MailboxSession, objectId);
			XmlElement xmlElement = xmlResponse.CreateElement("Folder", "FolderHierarchy:");
			foldersNode.AppendChild(xmlElement);
			XmlElement xmlElement2 = xmlResponse.CreateElement("DisplayName", "FolderHierarchy:");
			xmlElement2.InnerText = innerText2;
			xmlElement.AppendChild(xmlElement2);
			XmlElement xmlElement3 = xmlResponse.CreateElement("ServerId", "FolderHierarchy:");
			xmlElement3.InnerText = orAddSyncFolderId;
			xmlElement.AppendChild(xmlElement3);
			XmlElement xmlElement4 = xmlResponse.CreateElement("Type", "FolderHierarchy:");
			xmlElement4.InnerText = airSyncFolderType;
			xmlElement.AppendChild(xmlElement4);
			XmlElement xmlElement5 = xmlResponse.CreateElement("ParentId", "FolderHierarchy:");
			xmlElement5.InnerText = innerText;
			xmlElement.AppendChild(xmlElement5);
		}

		// Token: 0x04000611 RID: 1553
		private static readonly StorePropertyDefinition[] folderPropertyIds = new StorePropertyDefinition[]
		{
			FolderSchema.Id,
			StoreObjectSchema.ParentItemId,
			StoreObjectSchema.DisplayName,
			FolderSchema.FolderHierarchyDepth
		};

		// Token: 0x04000612 RID: 1554
		private SyncState folderIdMappingSyncState;

		// Token: 0x020000B6 RID: 182
		private enum FolderPropertyIdType
		{
			// Token: 0x04000614 RID: 1556
			Id,
			// Token: 0x04000615 RID: 1557
			ParentId,
			// Token: 0x04000616 RID: 1558
			DisplayName,
			// Token: 0x04000617 RID: 1559
			Depth
		}
	}
}
