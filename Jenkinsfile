pipeline {
  agent any
  stages {
    stage ('Initialize') {
      steps {
        echo 'GULSUM'
      }
    }
    stage ('for the jenkins branch') {
        when {
          branch "jenkins-*"
        }
        steps {
          bat  "dotnet restore ${workspace} blog_website\blog_website.sln"
        }
    }
    stage ('Build') {
      steps {
        cmd run.cmd
      }
    }
    stage ('for the PR') {
        when {
          branch 'PR-*'
        }
        steps {
          echo 'this only runs for the PRs'
        }
   }
  }
}