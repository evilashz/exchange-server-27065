using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using Microsoft.BDM.Pets.DNSManagement;
using Microsoft.BDM.Pets.SharedLibrary;
using Microsoft.BDM.Pets.SharedLibrary.Enums;
using www.microsoft.com.bdm.pets;

// Token: 0x02000BCE RID: 3022
[ServiceContract(Namespace = "http://www.microsoft.com/bdm/pets", ConfigurationName = "IDNSWebSvc")]
[GeneratedCode("System.ServiceModel", "3.0.0.0")]
public interface IDNSWebSvc
{
	// Token: 0x06004130 RID: 16688
	[FaultContract(typeof(BDMDNSFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/AddZoneBDMDNSFaultFault", Name = "BDMDNSFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(KeyNotFoundFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/AddZoneKeyNotFoundFaultFault", Name = "KeyNotFoundFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(DomainNameInUseFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/AddZoneDomainNameInUseFaultFault", Name = "DomainNameInUseFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(InvalidArgumentFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/AddZoneInvalidArgumentFaultFault", Name = "InvalidArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[OperationContract(Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/AddZone", ReplyAction = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/AddZoneResponse")]
	Guid AddZone(string domainName);

	// Token: 0x06004131 RID: 16689
	[FaultContract(typeof(DomainNameInUseFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/DeleteZoneDomainNameInUseFaultFault", Name = "DomainNameInUseFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(KeyNotFoundFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/DeleteZoneKeyNotFoundFaultFault", Name = "KeyNotFoundFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[OperationContract(Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/DeleteZone", ReplyAction = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/DeleteZoneResponse")]
	[FaultContract(typeof(InvalidArgumentFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/DeleteZoneInvalidArgumentFaultFault", Name = "InvalidArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(BDMDNSFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/DeleteZoneBDMDNSFaultFault", Name = "BDMDNSFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	void DeleteZone(Guid zoneGuid);

	// Token: 0x06004132 RID: 16690
	[FaultContract(typeof(InvalidArgumentFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/DeleteZoneByDomainNameInvalidArgumentFaultFault", Name = "InvalidArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(KeyNotFoundFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/DeleteZoneByDomainNameKeyNotFoundFaultFault", Name = "KeyNotFoundFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(DomainNameInUseFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/DeleteZoneByDomainNameDomainNameInUseFaultFault", Name = "DomainNameInUseFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(BDMDNSFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/DeleteZoneByDomainNameBDMDNSFaultFault", Name = "BDMDNSFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[OperationContract(Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/DeleteZoneByDomainName", ReplyAction = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/DeleteZoneByDomainNameResponse")]
	void DeleteZoneByDomainName(string domainName);

	// Token: 0x06004133 RID: 16691
	[FaultContract(typeof(KeyNotFoundFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/UpdateZoneKeyNotFoundFaultFault", Name = "KeyNotFoundFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(BDMDNSFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/UpdateZoneBDMDNSFaultFault", Name = "BDMDNSFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(InvalidArgumentFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/UpdateZoneInvalidArgumentFaultFault", Name = "InvalidArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[OperationContract(Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/UpdateZone", ReplyAction = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/UpdateZoneResponse")]
	[FaultContract(typeof(DomainNameInUseFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/UpdateZoneDomainNameInUseFaultFault", Name = "DomainNameInUseFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	void UpdateZone(Guid zoneGuid, bool isDisabled);

	// Token: 0x06004134 RID: 16692
	[FaultContract(typeof(KeyNotFoundFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/AddResourceRecordKeyNotFoundFaultFault", Name = "KeyNotFoundFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[OperationContract(Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/AddResourceRecord", ReplyAction = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/AddResourceRecordResponse")]
	[FaultContract(typeof(InvalidArgumentFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/AddResourceRecordInvalidArgumentFaultFault", Name = "InvalidArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(BDMDNSFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/AddResourceRecordBDMDNSFaultFault", Name = "BDMDNSFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(DomainNameInUseFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/AddResourceRecordDomainNameInUseFaultFault", Name = "DomainNameInUseFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	Guid AddResourceRecord(Guid zoneGuid, string domainName, int TTL, ResourceRecordType recordType, string value, bool deleteExisting);

	// Token: 0x06004135 RID: 16693
	[FaultContract(typeof(DomainNameInUseFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/AddResourceRecordByDomainNameDomainNameInUseFaultFault", Name = "DomainNameInUseFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[OperationContract(Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/AddResourceRecordByDomainName", ReplyAction = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/AddResourceRecordByDomainNameResponse")]
	[FaultContract(typeof(BDMDNSFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/AddResourceRecordByDomainNameBDMDNSFaultFault", Name = "BDMDNSFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(KeyNotFoundFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/AddResourceRecordByDomainNameKeyNotFoundFaultFault", Name = "KeyNotFoundFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(InvalidArgumentFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/AddResourceRecordByDomainNameInvalidArgumentFaultFault", Name = "InvalidArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	Guid[] AddResourceRecordByDomainName(string domainName, ResourceRecord[] records, bool[] deleteExisting);

	// Token: 0x06004136 RID: 16694
	[FaultContract(typeof(DomainNameInUseFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/DeleteResourceRecordDomainNameInUseFaultFault", Name = "DomainNameInUseFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(InvalidArgumentFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/DeleteResourceRecordInvalidArgumentFaultFault", Name = "InvalidArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(BDMDNSFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/DeleteResourceRecordBDMDNSFaultFault", Name = "BDMDNSFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(KeyNotFoundFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/DeleteResourceRecordKeyNotFoundFaultFault", Name = "KeyNotFoundFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[OperationContract(Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/DeleteResourceRecord", ReplyAction = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/DeleteResourceRecordResponse")]
	void DeleteResourceRecord(Guid resourceRecordId);

	// Token: 0x06004137 RID: 16695
	[FaultContract(typeof(KeyNotFoundFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/UpdateResourceRecordKeyNotFoundFaultFault", Name = "KeyNotFoundFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(BDMDNSFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/UpdateResourceRecordBDMDNSFaultFault", Name = "BDMDNSFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[OperationContract(Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/UpdateResourceRecord", ReplyAction = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/UpdateResourceRecordResponse")]
	[FaultContract(typeof(DomainNameInUseFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/UpdateResourceRecordDomainNameInUseFaultFault", Name = "DomainNameInUseFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(InvalidArgumentFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/UpdateResourceRecordInvalidArgumentFaultFault", Name = "InvalidArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	void UpdateResourceRecord(Guid resourceRecordId, int TTL, string value);

	// Token: 0x06004138 RID: 16696
	[FaultContract(typeof(DomainNameInUseFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/IsDomainAvailableDomainNameInUseFaultFault", Name = "DomainNameInUseFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[OperationContract(Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/IsDomainAvailable", ReplyAction = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/IsDomainAvailableResponse")]
	[FaultContract(typeof(InvalidArgumentFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/IsDomainAvailableInvalidArgumentFaultFault", Name = "InvalidArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(KeyNotFoundFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/IsDomainAvailableKeyNotFoundFaultFault", Name = "KeyNotFoundFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(BDMDNSFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/IsDomainAvailableBDMDNSFaultFault", Name = "BDMDNSFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	bool IsDomainAvailable(string domainName);

	// Token: 0x06004139 RID: 16697
	[FaultContract(typeof(InvalidArgumentFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetZoneInvalidArgumentFaultFault", Name = "InvalidArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(KeyNotFoundFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetZoneKeyNotFoundFaultFault", Name = "KeyNotFoundFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[OperationContract(Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetZone", ReplyAction = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetZoneResponse")]
	[FaultContract(typeof(DomainNameInUseFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetZoneDomainNameInUseFaultFault", Name = "DomainNameInUseFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(BDMDNSFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetZoneBDMDNSFaultFault", Name = "BDMDNSFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	Zone GetZone(Guid zoneGuid);

	// Token: 0x0600413A RID: 16698
	[OperationContract(Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetZoneByDomainName", ReplyAction = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetZoneByDomainNameResponse")]
	[FaultContract(typeof(InvalidArgumentFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetZoneByDomainNameInvalidArgumentFaultFault", Name = "InvalidArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(KeyNotFoundFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetZoneByDomainNameKeyNotFoundFaultFault", Name = "KeyNotFoundFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(DomainNameInUseFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetZoneByDomainNameDomainNameInUseFaultFault", Name = "DomainNameInUseFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(BDMDNSFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetZoneByDomainNameBDMDNSFaultFault", Name = "BDMDNSFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	Zone GetZoneByDomainName(string domainName);

	// Token: 0x0600413B RID: 16699
	[FaultContract(typeof(BDMDNSFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetResourceRecordsByZoneBDMDNSFaultFault", Name = "BDMDNSFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(KeyNotFoundFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetResourceRecordsByZoneKeyNotFoundFaultFault", Name = "KeyNotFoundFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(DomainNameInUseFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetResourceRecordsByZoneDomainNameInUseFaultFault", Name = "DomainNameInUseFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[OperationContract(Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetResourceRecordsByZone", ReplyAction = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetResourceRecordsByZoneResponse")]
	[FaultContract(typeof(InvalidArgumentFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetResourceRecordsByZoneInvalidArgumentFaultFault", Name = "InvalidArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	ResourceRecord[] GetResourceRecordsByZone(Guid zoneGuid);

	// Token: 0x0600413C RID: 16700
	[FaultContract(typeof(InvalidArgumentFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetAllResourceRecordsByDomainNameInvalidArgumentFaultFault", Name = "InvalidArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(BDMDNSFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetAllResourceRecordsByDomainNameBDMDNSFaultFault", Name = "BDMDNSFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(KeyNotFoundFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetAllResourceRecordsByDomainNameKeyNotFoundFaultFault", Name = "KeyNotFoundFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[OperationContract(Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetAllResourceRecordsByDomainName", ReplyAction = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetAllResourceRecordsByDomainNameResponse")]
	[FaultContract(typeof(DomainNameInUseFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetAllResourceRecordsByDomainNameDomainNameInUseFaultFault", Name = "DomainNameInUseFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	ResourceRecord[] GetAllResourceRecordsByDomainName(string domainName);

	// Token: 0x0600413D RID: 16701
	[FaultContract(typeof(BDMDNSFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetResourceRecordsByDomainNameBDMDNSFaultFault", Name = "BDMDNSFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(DomainNameInUseFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetResourceRecordsByDomainNameDomainNameInUseFaultFault", Name = "DomainNameInUseFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(KeyNotFoundFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetResourceRecordsByDomainNameKeyNotFoundFaultFault", Name = "KeyNotFoundFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[FaultContract(typeof(InvalidArgumentFault), Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetResourceRecordsByDomainNameInvalidArgumentFaultFault", Name = "InvalidArgumentFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.BDM.Pets.SharedLibrary")]
	[OperationContract(Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetResourceRecordsByDomainName", ReplyAction = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetResourceRecordsByDomainNameResponse")]
	ResourceRecord[] GetResourceRecordsByDomainName(string domainName, ResourceRecordType recordType);

	// Token: 0x0600413E RID: 16702
	[OperationContract(Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetHeader", ReplyAction = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/GetHeaderResponse")]
	BDMHeader GetHeader();

	// Token: 0x0600413F RID: 16703
	[OperationContract(Action = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/Ping", ReplyAction = "http://www.microsoft.com/bdm/pets/IDNSWebSvc/PingResponse")]
	bool Ping();
}
