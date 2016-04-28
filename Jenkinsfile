node('remote') {
	checkout scm: [$class: 'GitSCM', 
		branches: [[name: '*/master']], 
		userRemoteConfigs: [
			[
				url: 'https://github.com/pesetskyps/GH.3Tier.Demo.git',
				credentialsId: '2a536c43-287a-4bbe-aae6-092b2729e9e3'
			]
		]
	]

	try {
		bat("C:\\Windows\\system32\\WindowsPowerShell\\v1.0\\powershell.exe -ExecutionPolicy Bypass -Command sleep 1")
		bat("C:\\Windows\\system32\\WindowsPowerShell\\v1.0\\powershell.exe -ExecutionPolicy Bypass -Command throw 'fail'")
	}
	catch($err) {
		bat('git rev-parse HEAD > GIT_COMMIT')
		git_commit=readFile('GIT_COMMIT')
		short_commit=git_commit.take(6)

		bat("C:\\Windows\\system32\\WindowsPowerShell\\v1.0\\powershell.exe -ExecutionPolicy Bypass -File d:\\Dropbox\\plex\\scripts\\prepare_email.ps1 ${short_commit} \"${env.JENKINS_HOME}\" -rootUrl ${env.JENKINS_URL} -buildUrl ${env.BUILD_URL} -ProjectName ${env.JOB_NAME} -BuildResult \"FAILURE\"")
		$email_recepients = TrimEndOfLine(readFile("email_recepient_${short_commit}"))
		echo $email_recepients
		//"${TrimEndOfLine(readFile("email_recepient_${short_commit}"))}
		emailext body: "${readFile("email_template_${short_commit}")}", subject: "${env.JOB_NAME} FAILED for commit ${short_commit}", to: "pavel_pesetskiy@epam.com", replyTo: 'jenkins@plexdev.io', mimeType: 'text/html', attachLog: true
	}
}

def TrimEndOfLine($email_recepients)
{
	$email_recepients.replaceAll("[\n\r]", "");
}