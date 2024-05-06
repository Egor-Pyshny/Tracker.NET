/*City city = new City() { name = "city"};
 Country country = new Country() { name = "country" };
 Street street = new Street() { name = "street" };
 Address address = new Address() { 
     city = city,
     country = country,
     street = street,
     apartment = "apps",
     building = "bldng",
 };
 var d = new Diagnosis() { name = "test1", description = "as" };
 var d1 = new Diagnosis() { name = "test2", description = "as" };
 context.Diagnosis.Add(d);
 context.Diagnosis.Add(d1);
 Hospitals hospitals = new Hospitals()
 { name = "test1" , address = address};
 Hospitals hospitals1 = new Hospitals()
 { name = "test2" , address = address };
 context.Hospitals.Add(hospitals1);
 context.Hospitals.Add(hospitals);
 var t = new General_blood_analysis(){ date = DateTime.UtcNow, file_path = "test_path", patient = context.Patients.Where(p=>p.id==2).First()};
 var t1 = new General_urine_analysis(){ date = DateTime.UtcNow, file_path = "test_path", patient = context.Patients.Where(p=>p.id==2).First()};
 var t2 = new Xray_results(){ date = DateTime.UtcNow, file_path = "test_path", patient = context.Patients.Where(p=>p.id==2).First()};
 var t3 = new Appointment_records(){ 
     date = DateTime.UtcNow, 
     hospital = context.Hospitals.Where(h=>h.id==1).First(), 
     patient = context.Patients.Where(p=>p.id==2).First(),
     doctor = context.Doctors.Where(p => p.id == 1).First()
 };
 var s = new Surgery_reports() { file_path = "test_path" };
 var t = new Surgeries_history()
 {
     date = DateTime.UtcNow,
     doctors = context.Doctors.ToList(),
     patient = context.Patients.Where(p => p.id == 2).First(),
     hospital = context.Hospitals.Where(p => p.id == 2).First(),
     surgery_Report = s,
 };
 var t1 = new Surgeries_graphic()
 {
     date = DateTime.UtcNow,
     Doctors = context.Doctors.ToList(),
     patient = context.Patients.Where(p => p.id == 2).First(),
     hospital = context.Hospitals.Where(p => p.id == 2).First(),
 };
 context.Surgery_reports.Add(s);
 context.Surgeries_history.Add(t);
 context.Surgeries_graphic.Add(t1);
 context.SaveChanges();
 context.General_urine_analysis.Add(t1);
 context.General_blood_analysis.Add(t);
 context.Xray_results.Add(t2);
 context.Appointment_records.Add(t3);
 context.SaveChanges();*/