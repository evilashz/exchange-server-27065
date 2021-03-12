using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.BDM.Pets.DNSManagement;
using Microsoft.BDM.Pets.SharedLibrary.Enums;
using www.microsoft.com.bdm.pets;

// Token: 0x02000BD0 RID: 3024
[GeneratedCode("System.ServiceModel", "3.0.0.0")]
[DebuggerStepThrough]
public class DNSWebSvcClient : ClientBase<IDNSWebSvc>, IDNSWebSvc
{
	// Token: 0x06004140 RID: 16704 RVA: 0x000AD49D File Offset: 0x000AB69D
	public DNSWebSvcClient()
	{
	}

	// Token: 0x06004141 RID: 16705 RVA: 0x000AD4A5 File Offset: 0x000AB6A5
	public DNSWebSvcClient(string endpointConfigurationName) : base(endpointConfigurationName)
	{
	}

	// Token: 0x06004142 RID: 16706 RVA: 0x000AD4AE File Offset: 0x000AB6AE
	public DNSWebSvcClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x06004143 RID: 16707 RVA: 0x000AD4B8 File Offset: 0x000AB6B8
	public DNSWebSvcClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x06004144 RID: 16708 RVA: 0x000AD4C2 File Offset: 0x000AB6C2
	public DNSWebSvcClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
	{
	}

	// Token: 0x06004145 RID: 16709 RVA: 0x000AD4CC File Offset: 0x000AB6CC
	public Guid AddZone(string domainName)
	{
		return base.Channel.AddZone(domainName);
	}

	// Token: 0x06004146 RID: 16710 RVA: 0x000AD4DA File Offset: 0x000AB6DA
	public void DeleteZone(Guid zoneGuid)
	{
		base.Channel.DeleteZone(zoneGuid);
	}

	// Token: 0x06004147 RID: 16711 RVA: 0x000AD4E8 File Offset: 0x000AB6E8
	public void DeleteZoneByDomainName(string domainName)
	{
		base.Channel.DeleteZoneByDomainName(domainName);
	}

	// Token: 0x06004148 RID: 16712 RVA: 0x000AD4F6 File Offset: 0x000AB6F6
	public void UpdateZone(Guid zoneGuid, bool isDisabled)
	{
		base.Channel.UpdateZone(zoneGuid, isDisabled);
	}

	// Token: 0x06004149 RID: 16713 RVA: 0x000AD505 File Offset: 0x000AB705
	public Guid AddResourceRecord(Guid zoneGuid, string domainName, int TTL, ResourceRecordType recordType, string value, bool deleteExisting)
	{
		return base.Channel.AddResourceRecord(zoneGuid, domainName, TTL, recordType, value, deleteExisting);
	}

	// Token: 0x0600414A RID: 16714 RVA: 0x000AD51B File Offset: 0x000AB71B
	public Guid[] AddResourceRecordByDomainName(string domainName, ResourceRecord[] records, bool[] deleteExisting)
	{
		return base.Channel.AddResourceRecordByDomainName(domainName, records, deleteExisting);
	}

	// Token: 0x0600414B RID: 16715 RVA: 0x000AD52B File Offset: 0x000AB72B
	public void DeleteResourceRecord(Guid resourceRecordId)
	{
		base.Channel.DeleteResourceRecord(resourceRecordId);
	}

	// Token: 0x0600414C RID: 16716 RVA: 0x000AD539 File Offset: 0x000AB739
	public void UpdateResourceRecord(Guid resourceRecordId, int TTL, string value)
	{
		base.Channel.UpdateResourceRecord(resourceRecordId, TTL, value);
	}

	// Token: 0x0600414D RID: 16717 RVA: 0x000AD549 File Offset: 0x000AB749
	public bool IsDomainAvailable(string domainName)
	{
		return base.Channel.IsDomainAvailable(domainName);
	}

	// Token: 0x0600414E RID: 16718 RVA: 0x000AD557 File Offset: 0x000AB757
	public Zone GetZone(Guid zoneGuid)
	{
		return base.Channel.GetZone(zoneGuid);
	}

	// Token: 0x0600414F RID: 16719 RVA: 0x000AD565 File Offset: 0x000AB765
	public Zone GetZoneByDomainName(string domainName)
	{
		return base.Channel.GetZoneByDomainName(domainName);
	}

	// Token: 0x06004150 RID: 16720 RVA: 0x000AD573 File Offset: 0x000AB773
	public ResourceRecord[] GetResourceRecordsByZone(Guid zoneGuid)
	{
		return base.Channel.GetResourceRecordsByZone(zoneGuid);
	}

	// Token: 0x06004151 RID: 16721 RVA: 0x000AD581 File Offset: 0x000AB781
	public ResourceRecord[] GetAllResourceRecordsByDomainName(string domainName)
	{
		return base.Channel.GetAllResourceRecordsByDomainName(domainName);
	}

	// Token: 0x06004152 RID: 16722 RVA: 0x000AD58F File Offset: 0x000AB78F
	public ResourceRecord[] GetResourceRecordsByDomainName(string domainName, ResourceRecordType recordType)
	{
		return base.Channel.GetResourceRecordsByDomainName(domainName, recordType);
	}

	// Token: 0x06004153 RID: 16723 RVA: 0x000AD59E File Offset: 0x000AB79E
	public BDMHeader GetHeader()
	{
		return base.Channel.GetHeader();
	}

	// Token: 0x06004154 RID: 16724 RVA: 0x000AD5AB File Offset: 0x000AB7AB
	public bool Ping()
	{
		return base.Channel.Ping();
	}
}
