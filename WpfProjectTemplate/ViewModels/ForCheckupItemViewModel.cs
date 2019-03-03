using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using Caliburn.Micro;
using OutPatientApp.Models;

namespace OutPatientApp.ViewModels
{
    class ForCheckupItemViewModel : Screen, IEquatable<ForCheckupItemViewModel>
    {
        private string _fullName;
        private int _age;
        private string _address;
        private string _complaint;
        private string _diagnosis;
        private string _caseNumber;
        private string _pictureImage;
        private Guid _id;
        private Checkup Checkup { get; set; }

        public string FullName
        {
            get => _fullName;
            set => Set(ref _fullName, value);
        }

        public int Age
        {
            get => _age;
            set => Set(ref _age, value);
        }

        public string Address
        {
            get => _address;
            set => Set(ref _address, value);
        }

        public string Complaint
        {
            get => _complaint;
            set => Set(ref _complaint, value);
        }

        public string Diagnosis
        {
            get => _diagnosis;
            set => Set(ref _diagnosis, value);
        }

        public string CaseNumber
        {
            get => _caseNumber;
            set => Set(ref _caseNumber, value);
        }

        public string PictureImage
        {
            get => _pictureImage;
            set => Set(ref _pictureImage, value);
        }

        public Guid Id
        {
            get => _id;
            set => Set(ref _id, value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ForCheckupItemViewModel);
        }

        public bool Equals(ForCheckupItemViewModel other)
        {
            return other != null &&
                   FullName == other.FullName &&
                   Age == other.Age &&
                   Address == other.Address &&
                   Complaint == other.Complaint &&
                   Diagnosis == other.Diagnosis &&
                   CaseNumber == other.CaseNumber &&
                   PictureImage == other.PictureImage &&
                   Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            var hashCode = -506679544;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FullName);
            hashCode = hashCode * -1521134295 + Age.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Address);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Complaint);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Diagnosis);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CaseNumber);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PictureImage);
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(Id);
            return hashCode;
        }

        public static bool operator ==(ForCheckupItemViewModel model1, ForCheckupItemViewModel model2)
        {
            return EqualityComparer<ForCheckupItemViewModel>.Default.Equals(model1, model2);
        }

        public static bool operator !=(ForCheckupItemViewModel model1, ForCheckupItemViewModel model2)
        {
            return !(model1 == model2);
        }
    }
}
