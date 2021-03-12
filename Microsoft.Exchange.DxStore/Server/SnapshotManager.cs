using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.DxStore;
using Microsoft.Exchange.DxStore.Common;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.DxStore.Server
{
	// Token: 0x0200006A RID: 106
	public class SnapshotManager
	{
		// Token: 0x06000481 RID: 1153 RVA: 0x0000E51F File Offset: 0x0000C71F
		public SnapshotManager(DxStoreInstance instance)
		{
			this.instance = instance;
			this.SnapshotFileNameFullPath = this.GetSnapshotFileName();
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000E545 File Offset: 0x0000C745
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.SnapshotTracer;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000E54C File Offset: 0x0000C74C
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0000E554 File Offset: 0x0000C754
		public string SnapshotFileNameFullPath { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000E55D File Offset: 0x0000C75D
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x0000E565 File Offset: 0x0000C765
		public bool IsInitialLoadAttempted { get; set; }

		// Token: 0x06000487 RID: 1159 RVA: 0x0000E578 File Offset: 0x0000C778
		public void InitializeDataStore()
		{
			this.instance.RunBestEffortOperation("LoadSnapshot", delegate
			{
				this.InitializeDataStoreInternal();
			}, LogOptions.LogAll, null, null, null, null);
			this.IsInitialLoadAttempted = true;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000E5C8 File Offset: 0x0000C7C8
		public void Start()
		{
			lock (this.locker)
			{
				if (this.timer == null)
				{
					this.timer = new GuardedTimer(delegate(object unused)
					{
						this.SaveSnapshotCallback();
					}, null, TimeSpan.Zero, this.instance.GroupConfig.Settings.SnapshotUpdateInterval);
				}
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000E644 File Offset: 0x0000C844
		public void Stop()
		{
			lock (this.locker)
			{
				if (this.timer != null)
				{
					this.timer.Dispose(true);
					this.timer = null;
				}
			}
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000E69C File Offset: 0x0000C89C
		public void ApplySnapshot(InstanceSnapshotInfo snapshotInfo, bool isTakeSnapshot = true)
		{
			lock (this.locker)
			{
				this.instance.LocalDataStore.ApplySnapshot(snapshotInfo, null);
				if (isTakeSnapshot)
				{
					this.SaveSnapshot(true);
				}
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000E6FC File Offset: 0x0000C8FC
		public void ForceFlush()
		{
			this.SaveSnapshot(true);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000E710 File Offset: 0x0000C910
		public void SaveSnapshotCallback()
		{
			Utils.RunOperation(this.instance.Identity, "SaveSnapshot", delegate
			{
				this.SaveSnapshot(false);
			}, this.instance.EventLogger, LogOptions.LogException | this.instance.GroupConfig.Settings.AdditionalLogOptions, true, null, null, null, null, null);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000E778 File Offset: 0x0000C978
		private void InitializeDataStoreInternal()
		{
			int num = 0;
			XElement xelementFromSnapshot = this.GetXElementFromSnapshot(this.SnapshotFileNameFullPath);
			if (xelementFromSnapshot != null)
			{
				XAttribute xattribute = xelementFromSnapshot.Attribute("LastInstanceExecuted");
				if (xattribute != null)
				{
					num = int.Parse(xattribute.Value);
				}
				XElement xelement = xelementFromSnapshot.Elements().FirstOrDefault<XElement>();
				if (xelement != null)
				{
					SnapshotManager.Tracer.Information<string, int>((long)this.instance.IdentityHash, "{0}: Startup - Applying snapshot to local store (LastInstanceNumber: {1})", this.instance.Identity, num);
					this.instance.LocalDataStore.ApplySnapshotFromXElement("\\", num, xelement);
				}
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000E804 File Offset: 0x0000CA04
		private void SaveSnapshot(bool isForce = false)
		{
			DataStoreStats storeStats = this.instance.LocalDataStore.GetStoreStats();
			if (isForce || this.lastRecordedStoreStats == null || storeStats.LastUpdateNumber > this.lastRecordedStoreStats.LastUpdateNumber || storeStats.LastUpdateTime > this.lastRecordedStoreStats.LastUpdateTime)
			{
				SnapshotManager.Tracer.Information<string, int, DateTimeOffset>((long)this.instance.IdentityHash, "{0}: Attempting to take snapshot (RecentUpdate: {1}, RecentUpdateTime: {2})", this.instance.Identity, storeStats.LastUpdateNumber, storeStats.LastUpdateTime);
				this.lastRecordedStoreStats = storeStats.Clone();
				this.CreateSnapshotDirectoryIfRequired();
				int num;
				XElement xelementSnapshot = this.instance.LocalDataStore.GetXElementSnapshot(null, out num);
				XElement xelement = new XElement("SnapshotRoot", new XAttribute("LastInstanceExecuted", num));
				xelement.Add(xelementSnapshot);
				string text = xelement.ToString();
				SnapshotManager.Tracer.Information<string, int, string>((long)this.instance.IdentityHash, "{0}: Writing {1} chars to {2}", this.instance.Identity, text.Length, this.SnapshotFileNameFullPath);
				File.WriteAllText(this.SnapshotFileNameFullPath, text, Encoding.UTF8);
				return;
			}
			SnapshotManager.Tracer.Information<string, int, DateTimeOffset>((long)this.instance.IdentityHash, "{0}: Skipped saving snapshot since there are no changes observed (LastUpdate: {1}, LastUpdateTime: {2})", this.instance.Identity, storeStats.LastUpdateNumber, storeStats.LastUpdateTime);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000E95F File Offset: 0x0000CB5F
		private void CreateSnapshotDirectoryIfRequired()
		{
			if (!Directory.Exists(this.instance.GroupConfig.Settings.SnapshotStorageDir))
			{
				Directory.CreateDirectory(this.instance.GroupConfig.Settings.SnapshotStorageDir);
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000E998 File Offset: 0x0000CB98
		private XElement GetXElementFromSnapshot(string fileName)
		{
			Exception ex = null;
			if (!File.Exists(fileName))
			{
				return null;
			}
			XElement result = null;
			try
			{
				string text = File.ReadAllText(fileName, Encoding.UTF8);
				result = XElement.Parse(text);
			}
			catch (XmlException ex2)
			{
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				SnapshotManager.Tracer.TraceError<string, string, string>((long)this.instance.IdentityHash, "{0}: Parse/read of {1} failed with {2}", this.instance.Identity, this.SnapshotFileNameFullPath, ex.Message);
				this.instance.EventLogger.Log(DxEventSeverity.Error, 0, "{0}: Failed to read snapshot from {1} (error: {2})", new object[]
				{
					this.instance.Identity,
					this.SnapshotFileNameFullPath,
					ex.Message
				});
			}
			return result;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000EA6C File Offset: 0x0000CC6C
		private string GetSnapshotFileName()
		{
			return Utils.CombinePathNullSafe(this.instance.GroupConfig.Settings.SnapshotStorageDir, this.instance.GroupConfig.Settings.DefaultSnapshotFileName);
		}

		// Token: 0x0400021C RID: 540
		public const string LastInstanceExecutedAttribute = "LastInstanceExecuted";

		// Token: 0x0400021D RID: 541
		private readonly object locker = new object();

		// Token: 0x0400021E RID: 542
		private readonly DxStoreInstance instance;

		// Token: 0x0400021F RID: 543
		private DataStoreStats lastRecordedStoreStats;

		// Token: 0x04000220 RID: 544
		private GuardedTimer timer;
	}
}
