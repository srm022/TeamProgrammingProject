export class ValidationPatterns {
    public noteContent = /^.{1,512}$/;
    public groupDescription = /^.{1,256}$/;
    public objectName = /^.{1,128}$/;
    public emailPattern = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
}