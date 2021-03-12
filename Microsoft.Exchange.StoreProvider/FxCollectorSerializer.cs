using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x0200005C RID: 92
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FxCollectorSerializer : IMapiFxCollector
	{
		// Token: 0x0600026C RID: 620 RVA: 0x0000B044 File Offset: 0x00009244
		public FxCollectorSerializer(IMapiFxProxy fxProxy)
		{
			this.fxProxy = fxProxy;
			byte[] objectData = this.fxProxy.GetObjectData();
			BinaryDeserializer.Deserialize(objectData, delegate(BinaryDeserializer deserializer)
			{
				this.cachedObjectType = deserializer.ReadGuid();
				this.cachedServerVersion = deserializer.ReadBytes();
				if (this.cachedObjectType == InterfaceIds.IMsgStoreGuid)
				{
					this.cachedIsPrivateLogon = (deserializer.ReadInt() != 0);
					return;
				}
				this.cachedIsPrivateLogon = false;
			});
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000B083 File Offset: 0x00009283
		private void DoOperation(FxOpcodes opCode, byte[] request)
		{
			this.fxProxy.ProcessRequest(opCode, request);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000B092 File Offset: 0x00009292
		public Guid GetObjectType()
		{
			return this.cachedObjectType;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000B09A File Offset: 0x0000929A
		public byte[] GetServerVersion()
		{
			return this.cachedServerVersion;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000B0A2 File Offset: 0x000092A2
		public bool IsPrivateLogon()
		{
			return this.cachedIsPrivateLogon;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000B0CC File Offset: 0x000092CC
		public void Config(int flags, int transferMethod)
		{
			byte[] request = BinarySerializer.Serialize(delegate(BinarySerializer serializer)
			{
				serializer.Write(flags);
				serializer.Write(transferMethod);
			});
			this.DoOperation(FxOpcodes.Config, request);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000B107 File Offset: 0x00009307
		public void TransferBuffer(byte[] data)
		{
			this.DoOperation(FxOpcodes.TransferBuffer, data);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000B140 File Offset: 0x00009340
		public void IsInterfaceOk(int transferMethod, Guid refiid, int flags)
		{
			byte[] request = BinarySerializer.Serialize(delegate(BinarySerializer serializer)
			{
				serializer.Write(transferMethod);
				serializer.Write(refiid);
				serializer.Write(flags);
			});
			this.DoOperation(FxOpcodes.IsInterfaceOk, request);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000B182 File Offset: 0x00009382
		public void TellPartnerVersion(byte[] versionData)
		{
			this.DoOperation(FxOpcodes.TellPartnerVersion, versionData);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000B18C File Offset: 0x0000938C
		public void StartMdbEventsImport()
		{
			this.DoOperation(FxOpcodes.StartMdbEventsImport, null);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000B1B4 File Offset: 0x000093B4
		public void FinishMdbEventsImport(bool success)
		{
			byte[] request = BinarySerializer.Serialize(delegate(BinarySerializer serializer)
			{
				serializer.Write(success ? 1 : 0);
			});
			this.DoOperation(FxOpcodes.FinishMdbEventsImport, request);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000B1E9 File Offset: 0x000093E9
		public void AddMdbEvents(byte[] request)
		{
			this.DoOperation(FxOpcodes.AddMdbEvents, request);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000B260 File Offset: 0x00009460
		public void SetWatermarks(MDBEVENTWMRAW[] WMs)
		{
			byte[] request = BinarySerializer.Serialize(delegate(BinarySerializer serializer)
			{
				serializer.Write(WMs.Length);
				foreach (MDBEVENTWMRAW mdbeventwmraw in WMs)
				{
					serializer.Write(mdbeventwmraw.guidMailbox);
					serializer.Write(mdbeventwmraw.guidConsumer);
					serializer.Write(mdbeventwmraw.eventCounter);
				}
			});
			this.DoOperation(FxOpcodes.SetWatermarks, request);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000B2B8 File Offset: 0x000094B8
		public void SetReceiveFolder(byte[] entryId, string messageClass)
		{
			byte[] request = BinarySerializer.Serialize(delegate(BinarySerializer serializer)
			{
				serializer.Write(entryId);
				serializer.Write(messageClass);
			});
			this.DoOperation(FxOpcodes.SetReceiveFolder, request);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000B364 File Offset: 0x00009564
		public void SetPerUser(MapiLtidNative ltid, Guid guidReplica, int lib, byte[] pb, bool fLast)
		{
			byte[] request = BinarySerializer.Serialize(delegate(BinarySerializer serializer)
			{
				serializer.Write(ltid.replGuid);
				serializer.Write(ltid.globCount);
				serializer.Write(guidReplica);
				serializer.Write(lib);
				serializer.Write(pb);
				serializer.Write(fLast ? 1 : 0);
			});
			this.DoOperation(FxOpcodes.SetPerUser, request);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000B3D0 File Offset: 0x000095D0
		public void SetProps(PropValue[] pva)
		{
			byte[] request = BinarySerializer.Serialize(delegate(BinarySerializer serializer)
			{
				serializer.Write(pva);
			});
			this.DoOperation(FxOpcodes.SetProps, request);
		}

		// Token: 0x04000460 RID: 1120
		protected IMapiFxProxy fxProxy;

		// Token: 0x04000461 RID: 1121
		protected Guid cachedObjectType;

		// Token: 0x04000462 RID: 1122
		protected byte[] cachedServerVersion;

		// Token: 0x04000463 RID: 1123
		protected bool cachedIsPrivateLogon;
	}
}
