pipeline {
  agent any
  stages {
    stage ('Build') {
      steps {
        sh 'dotnet build'
      }
    }
    stage ('Restore') {
      steps {
        sh 'dotnet restore'
      }
    }
    stage ('Run') {
      steps {
        sh 'dotnet run --project blog_website/blog_website.csproj'
      }
    }
  }
}
