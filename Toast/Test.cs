using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Toast
{
    public class Test
    {

        public class Rootobject
        {
            public Club[] Clubs { get; set; }
            public Consent[] Consents { get; set; }
            public Contactinfo ContactInfo { get; set; }
            public Currentlogininformation CurrentLoginInformation { get; set; }
            public Education[] Education { get; set; }
            public Id2 Id { get; set; }
            public Identity Identity { get; set; }
            public DateTime MemberSince { get; set; }
            public Signees Signees { get; set; }
        }

        public class Contactinfo
        {
            public string GivenName { get; set; }
            public string MiddleName { get; set; }
            public object NameSuffix { get; set; }
            public string Surname { get; set; }
            //public Address Address { get; set; }
            public Digitalcontact[] DigitalContacts { get; set; }
            public string DisplayName { get; set; }
            public Phonenumber[] PhoneNumbers { get; set; }
        }

        //public class Address
        //{
        //    public Address1 Address { get; set; }
        //    public object Company { get; set; }
        //    public string AddressedTo { get; set; }
        //    public object AttentionLine { get; set; }
        //}

        public class Address1
        {
            public Id Id { get; set; }
            public string Street { get; set; }
            public object[] AdditionalLines { get; set; }
            public string City { get; set; }
            public object County { get; set; }
            public Primaryregion PrimaryRegion { get; set; }
            public object PrimaryRegionDescription { get; set; }
            public Country Country { get; set; }
            public string PostalCode { get; set; }
            public object Coordinates { get; set; }
            public object TimeZone { get; set; }
            public object ValidationDetails { get; set; }
        }

        public class Id
        {
            public int Value { get; set; }
        }

        public class Primaryregion
        {
            public string Value { get; set; }
        }

        public class Country
        {
            public string Value { get; set; }
        }

        public class Digitalcontact
        {
            public string RawText { get; set; }
        }

        public class Phonenumber
        {
            public int Type { get; set; }
            public string RawText { get; set; }
            public Regioncode RegionCode { get; set; }
        }

        public class Regioncode
        {
            public string Value { get; set; }
        }

        public class Currentlogininformation
        {
            public Email Email { get; set; }
            public ID1 ID { get; set; }
            public string UserName { get; set; }
        }

        public class Email
        {
            public string RawText { get; set; }
        }

        public class ID1
        {
            public string Value { get; set; }
        }

        public class Id2
        {
            public string Value { get; set; }
        }

        public class Identity
        {
            public object NamePrefix { get; set; }
            public string GivenName { get; set; }
            public string MiddleName { get; set; }
            public string Surname { get; set; }
            public object NameSuffix { get; set; }
            public Gender Gender { get; set; }
            public string Credentials { get; set; }
        }

        public class Gender
        {
            public string Value { get; set; }
        }

        public class Signees
        {
            public _03891851 _03891851 { get; set; }
        }

        public class _03891851
        {
            public object NamePrefix { get; set; }
            public string GivenName { get; set; }
            public string MiddleName { get; set; }
            public string Surname { get; set; }
            public object NameSuffix { get; set; }
            public Gender1 Gender { get; set; }
            public string Credentials { get; set; }
        }

        public class Gender1
        {
            public string Value { get; set; }
        }

        public class Club
        {
            public Id3 Id { get; set; }
            public string Name { get; set; }
            public bool IsOnline { get; set; }
        }

        public class Id3
        {
            public string Value { get; set; }
        }

        public class Consent
        {
            public Signedby SignedBy { get; set; }
            public DateTime? SignedOn { get; set; }
            public bool Consented { get; set; }
        }

        public class Signedby
        {
            public string Value { get; set; }
        }

        public class Education
        {
            public bool Complete { get; set; }
            //public Language Language { get; set; }
            public object[] LevelAwards { get; set; }
            public int Medium { get; set; }
            public Path Path { get; set; }
        }

        //public class Language
        //{
        //    public Language1 Language { get; set; }
        //    public int Type { get; set; }
        //}

        public class Language1
        {
            public string Value { get; set; }
        }

        public class Path
        {
            public int Value { get; set; }
        }

    }
}